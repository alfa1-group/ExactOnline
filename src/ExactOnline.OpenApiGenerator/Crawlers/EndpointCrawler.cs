using System.Net.Mime;
using System.Text.RegularExpressions;
using ExactOnline.OpenApiGenerator.Extensions;
using ExactOnline.OpenApiGenerator.HtmlDocumentLoaders;
using ExactOnline.OpenApiGenerator.Parsers;
using HtmlAgilityPack;
using Humanizer;
using Microsoft.OpenApi;
using MonkeyCache.FileStore;

namespace ExactOnline.OpenApiGenerator.Crawlers;

internal class EndpointCrawler
{
    private const int MaxRetries = 3;
    private static readonly Regex EndpointUriRegex = new(@"\{(\w+)\}", RegexOptions.Compiled);
    private static readonly Regex EndpointUriEdmTypeRegex = new(@"(\w+)=\{([^}]+)\}", RegexOptions.Compiled);
    private static readonly string[] ReturnsSingleItem = ["SystemSystemMe", "ReadSyncSyncSyncTimestamp"];

    private readonly OpenApiDocument _openApiDoc;
    private readonly PuppeteerHtmlLoader _puppeteerHtmlLoader;
    private readonly IReadOnlyList<string> _urls;
    private readonly bool _useCache;

    internal EndpointCrawler(PuppeteerHtmlLoader puppeteerHtmlLoader, IReadOnlyList<string> urls, bool useCache)
    {
        _puppeteerHtmlLoader = puppeteerHtmlLoader;
        _urls = urls;
        _useCache = useCache;

        var metadata = new OpenApiSchema
        {
            Type = JsonSchemaType.Object,
            ReadOnly = true,
            Required = new HashSet<string> { "uri", "type" },
            Properties = new Dictionary<string, IOpenApiSchema>
            {
                { "uri", new OpenApiSchema { Type = JsonSchemaType.String } },
                { "type", new OpenApiSchema { Type = JsonSchemaType.String } }
            }
        };

        var error = new OpenApiSchema
        {
            Type = JsonSchemaType.Object,
            Properties = new Dictionary<string, IOpenApiSchema>
            {
                ["error"] = new OpenApiSchema
                {
                    Type = JsonSchemaType.Object,
                    ReadOnly = true,
                    Properties = new Dictionary<string, IOpenApiSchema>
                    {
                        ["code"] = new OpenApiSchema
                        {
                            Type = JsonSchemaType.String,
                            ReadOnly = true,
                            Description = "Service-defined error code"
                        },
                        ["message"] = new OpenApiSchema
                        {
                            Type = JsonSchemaType.Object,
                            ReadOnly = true,
                            Properties = new Dictionary<string, IOpenApiSchema>
                            {
                                ["lang"] = new OpenApiSchema
                                {
                                    Type = JsonSchemaType.String,
                                    ReadOnly = true,
                                    Description = "Language code (e.g., en-us)"
                                },
                                ["value"] = new OpenApiSchema
                                {
                                    Type = JsonSchemaType.String,
                                    ReadOnly = true,
                                    Description = "A human-readable error message"
                                }
                            },
                            Required = new HashSet<string> { "lang", "value" }
                        }
                    },
                    Required = new HashSet<string> { "code", "message" }
                }
            },
            Required = new HashSet<string> { "error" }
        };

        _openApiDoc = new OpenApiDocument
        {
            Info = new OpenApiInfo
            {
                Title = "Exact Online REST API",
                Version = "0.0.1"
            },
            Servers = new List<OpenApiServer>
            {
                new()
                {
                    Url = "https://start.exactonline.nl",
                    Description = "Exact Online REST API Endpoint"
                }
            },
            Paths = new OpenApiPaths(),
            Components = new OpenApiComponents
            {
                Schemas = new Dictionary<string, IOpenApiSchema>
                {
                    { "ExactOnlineMetadata", metadata },
                    { "ODataError", error }
                }
            }
        };
    }

    internal async Task<OpenApiDocument> CrawlAndProcessAsync(Action<string> onEndpointProcessing, CancellationToken cancellationToken)
    {
        await using var htmlLoader = new PuppeteerHtmlLoader();

        foreach (var url in _urls)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            onEndpointProcessing(url);

            var contentDictionary = await GetDocsAsync(url, cancellationToken);

            var docs = contentDictionary.ToDictionary(
                pair => HttpMethod.Parse(pair.Key),
                pair =>
                {
                    var doc = new HtmlDocument();
                    doc.LoadHtml(pair.Value);
                    return doc;
                });

            Process(url, docs, _openApiDoc);
        }

        return _openApiDoc;
    }

    private async Task<IDictionary<string, string>> GetDocsAsync(string url, CancellationToken cancellationToken)
    {
        if (_useCache && !Barrel.Current.IsExpired(key: url))
        {
            return Barrel.Current.Get<IDictionary<string, string>>(key: url);
        }

        IDictionary<string, string> contentDictionary;
        var retries = 0;
        while (true)
        {
            try
            {
                contentDictionary = await _puppeteerHtmlLoader.LoadAsync(url, cancellationToken);
                break;
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
            {
                retries++;

                if (retries >= MaxRetries)
                {
                    Console.WriteLine($"Failed to load {url} after {MaxRetries} attempts.");
                    throw;
                }

                await Task.Delay((int)Math.Pow(2, retries) * 1000, cancellationToken);
            }
        }

        Barrel.Current.Add(key: url, data: contentDictionary, expireIn: TimeSpan.FromDays(365));

        return contentDictionary;
    }

    private static string GetResponseDescription(string baseSchemaName, bool returnsMultiple)
    {
        return returnsMultiple ? $"A collection of {baseSchemaName} entities." : $"The {baseSchemaName} entity.";
    }

    private static void Process(string pageUrl, IDictionary<HttpMethod, HtmlDocument> docs, OpenApiDocument openApiDoc)
    {
        var docGet = docs[HttpMethod.Get];

        var nameFromUrl = pageUrl.Split("?name=").Last().Trim();
        var isSingleResponse = ReturnsSingleItem.Contains(nameFromUrl);

        var baseSchemaName = nameFromUrl.Singularize();
        var endpointDescription = docGet.DocumentNode.SelectSingleNode("//p[@id='goodToKnow']")?.InnerText.Trim() ?? string.Empty;
        var (baseEndpointUri, queryParameters) = GetEndpointUriDetails(docGet);
        var isSyncInterface = baseSchemaName.StartsWith("Sync");
        var isGetOnly = docs.Count == 1;

        var metaDataRef = new OpenApiSchemaReference("ExactOnlineMetadata")
        {
            ReadOnly = isGetOnly
        };

        foreach (var (httpMethod, document) in docs)
        {
            var properties = new Dictionary<string, IOpenApiSchema>
            {
                { "__metadata", metaDataRef }
            };

            string? keyName = null;
            IOpenApiSchema? keyType = null;

            var requiredProperties = new HashSet<string>();

            // Rows from the tabel which are not hidden
            var tableRows = document.DocumentNode.SelectNodes("//table[@id='referencetable']//tr[not(contains(@style, 'display: none'))]");
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (tableRows != null)
            {
                var nameColumnIndex = 1;
                var typeColumnIndex = 5;
                var descriptionColumnIndex = tableRows.FirstOrDefault()?.SelectNodes("th")
                    .Select((node, idx) => new { node, idx })
                    .Where(x => x.node.InnerText.Contains("Description"))
                    .Select(x => x.idx)
                    .FirstOrDefault();

                var propertyRows = tableRows.Skip(1).ToArray();

                foreach (var row in propertyRows)
                {
                    var rowIsKey = row.GetClasses().Contains("key");

                    var columns = row.SelectNodes("td");
                    if (columns is { Count: >= 7 })
                    {
                        var nameColumn = columns[nameColumnIndex];
                        var name = nameColumn.InnerText.Trim();
                        var linkNode = nameColumn.SelectSingleNode(".//a");
                        var href = linkNode?.Attributes["href"]?.Value;
                        var linkedSchemaName = href?.Split("?name=").Last().Trim();

                        var type = columns[typeColumnIndex].InnerText.Trim().Split(' ')[0].Trim();
                        var description = descriptionColumnIndex.HasValue ? columns[descriptionColumnIndex.Value].InnerText.Trim() : string.Empty;
                        var isCollection = description.Contains("collection of", StringComparison.OrdinalIgnoreCase);
                        var isRequired = rowIsKey || bool.TryParse(columns[2].InnerText.Trim(), out var isMandatoryValue) && isMandatoryValue;

                        if (string.IsNullOrEmpty(name))
                        {
                            continue;
                        }

                        if (!EdmTypeParser.TryParse(type, description, isGetOnly, out IOpenApiSchema? property))
                        {
                            if (!string.IsNullOrEmpty(linkedSchemaName))
                            {
                                if (isCollection)
                                {
                                    property = new OpenApiSchema
                                    {
                                        Type = JsonSchemaType.Array,
                                        Description = description,
                                        Items = new OpenApiSchemaReference(linkedSchemaName),
                                        ReadOnly = isGetOnly
                                    };
                                }
                                else
                                {
                                    property = new OpenApiSchemaReference(linkedSchemaName)
                                    {
                                        ReadOnly = isGetOnly
                                    };
                                }
                            }
                            else
                            {
                                if (isCollection)
                                {
                                    property = new OpenApiSchema
                                    {
                                        Type = JsonSchemaType.Array,
                                        Description = description,
                                        Items = new OpenApiSchema
                                        {
                                            Type = JsonSchemaType.Object
                                        },
                                        ReadOnly = isGetOnly
                                    };
                                }
                                else
                                {
                                    property = new OpenApiSchema
                                    {
                                        Type = JsonSchemaType.Object,
                                        Description = description,
                                        ReadOnly = isGetOnly
                                    };
                                }
                            }
                        }

                        if (isRequired)
                        {
                            requiredProperties.Add(name);
                        }

                        properties.Add(name, property);

                        if (rowIsKey)
                        {
                            keyName = name;
                            keyType = property;
                        }
                    }
                }
            }

            var entityComponent = new OpenApiSchema
            {
                Type = JsonSchemaType.Object,
                Properties = properties,
                Required = requiredProperties
            };

            var schemaName = httpMethod == HttpMethod.Get ? baseSchemaName : baseSchemaName + httpMethod.ToString().ToPascalCase();
            openApiDoc.Components!.Schemas!.Add(schemaName, entityComponent);

            AddOperations(openApiDoc, httpMethod, baseEndpointUri, baseSchemaName, endpointDescription, queryParameters, keyName, keyType, isSyncInterface, isSingleResponse);
        }
    }

    private static OpenApiOperation CreateDefaultOperation(HttpMethod httpMethod, string baseSchemaName, string baseEndpointUri)
    {
        var oDataErrorRef = new OpenApiSchemaReference("ODataError")
        {
            ReadOnly = true
        };

        var errorContent = new Dictionary<string, OpenApiMediaType>
        {
            { MediaTypeNames.Application.Json, new OpenApiMediaType { Schema = oDataErrorRef } }
        };

        var error400Response = new OpenApiResponse
        {
            Description = $"Bad request: {httpMethod} operation failed",
            Content = errorContent
        };

        var error500Response = new OpenApiResponse
        {
            Description = $"Internal server error: {httpMethod} operation failed",
            Content = errorContent
        };

        var operation = new OpenApiOperation
        {
            Summary = $"{httpMethod} {baseSchemaName}",
            Parameters = new List<IOpenApiParameter>(),
            Responses = new OpenApiResponses
            {
                { "400", error400Response },
                { "500", error500Response }
            }
        };

        var matches = EndpointUriRegex.Matches(baseEndpointUri);
        foreach (Match match in matches)
        {
            var name = match.Groups[1].Value;
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = name,
                In = ParameterLocation.Path,
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = name == "division" ? JsonSchemaType.Integer : JsonSchemaType.String
                }
            });
        }

        return operation;
    }

    private static void AddOperations(
        OpenApiDocument openApiDoc,
        HttpMethod httpMethod,
        string baseEndpointUri,
        string baseSchemaName,
        string endpointDescription,
        HashSet<OpenApiParameter> queryParameters,
        string? keyName,
        IOpenApiSchema? keyType,
        bool isSyncInterface,
        bool isSingleResponse
    )
    {
        var schemaNameWithHttpMethod = baseSchemaName + httpMethod.ToString().ToPascalCase();

        if (httpMethod == HttpMethod.Get)
        {
            var schemaRef = new OpenApiSchemaReference(baseSchemaName)
            {
                ReadOnly = true
            };

            var arrayResponse = new OpenApiSchema
            {
                Type = JsonSchemaType.Array,
                ReadOnly = true,
                Items = schemaRef
            };
            openApiDoc.Components!.Schemas!.Add(baseSchemaName + "_Array", arrayResponse);

            var arrayResponseRef = new OpenApiSchemaReference(baseSchemaName + "_Array");

            var resultsResponse = new OpenApiSchema
            {
                Type = JsonSchemaType.Object,
                ReadOnly = true,
                Properties = new Dictionary<string, IOpenApiSchema>
                    {
                        { "results", arrayResponseRef },
                        { "__next", new OpenApiSchema
                            {
                                Type = JsonSchemaType.String,
                                ReadOnly = true,
                                Description = "This property contains a link to request the next set of records including the option which are passed in the initial request with a $skiptoken option."
                            }
                        }
                    },
                Required = new HashSet<string> { "results" }
            };
            openApiDoc.Components!.Schemas!.Add(baseSchemaName + "_Results", resultsResponse);

            var resultsResponseRef = new OpenApiSchemaReference(baseSchemaName + "_Results");

            var getResponseSchema = new OpenApiSchema
            {
                Description = GetResponseDescription(baseSchemaName, !isSingleResponse),
                Type = JsonSchemaType.Object,
                ReadOnly = true,
                Properties = new Dictionary<string, IOpenApiSchema>
                {
                    { "d", new OpenApiSchema
                        {
                            OneOf =
                            [
                                arrayResponseRef,
                                resultsResponseRef
                            ],
                            Discriminator = new OpenApiDiscriminator
                            {
                                PropertyName = "results",
                                Mapping = new Dictionary<string, OpenApiSchemaReference>
                                {
                                    { "_Results", resultsResponseRef }
                                }
                            }
                        }
                    }
                },
                Required = new HashSet<string> { "d" }
            };
            openApiDoc.Components!.Schemas!.Add(baseSchemaName + "_Response", getResponseSchema);
        }
        else if (httpMethod == HttpMethod.Post)
        {
            var schemaRef = new OpenApiSchemaReference(schemaNameWithHttpMethod)
            {
                ReadOnly = true
            };
            var postResponseSchema = new OpenApiSchema
            {
                Description = GetResponseDescription(baseSchemaName, false),
                Type = JsonSchemaType.Object,
                ReadOnly = true,
                Properties = new Dictionary<string, IOpenApiSchema>
                {
                    { "d", schemaRef }
                },
                Required = new HashSet<string> { "d" }
            };
            openApiDoc.Components!.Schemas!.Add(schemaNameWithHttpMethod + "_Response", postResponseSchema);
        }

        if (httpMethod == HttpMethod.Get)
        {
            var getOperation = CreateDefaultOperation(httpMethod, baseSchemaName, baseEndpointUri);

            foreach (var queryParameter in queryParameters)
            {
                getOperation.Parameters!.Add(queryParameter);
            }

            AddODataQueryParameters(getOperation.Parameters!, isSyncInterface);

            var getResponse = new OpenApiResponse
            {
                Description = $"{httpMethod} operation successful",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    {
                        MediaTypeNames.Application.Json, new OpenApiMediaType
                        {
                            Schema = new OpenApiSchemaReference(baseSchemaName + "_Response")
                            {
                                ReadOnly = true
                            }
                        }
                    }
                }
            };
            getOperation.Responses!.Add("200", getResponse);

            AddOperationToPath(openApiDoc, httpMethod, baseEndpointUri, endpointDescription, getOperation);
        }
        else if (httpMethod == HttpMethod.Post)
        {
            var schemaRef = new OpenApiSchemaReference(schemaNameWithHttpMethod)
            {
                ReadOnly = true
            };
            var postOperation = CreateDefaultOperation(httpMethod, baseSchemaName, baseEndpointUri);
            postOperation.RequestBody = new OpenApiRequestBody
            {
                Description = $"The {baseSchemaName} entity to create.",
                Required = true,
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    { MediaTypeNames.Application.Json, new OpenApiMediaType { Schema = schemaRef } }
                }
            };

            var postResponse = new OpenApiResponse
            {
                Description = $"{httpMethod} operation successful",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    {
                        MediaTypeNames.Application.Json, new OpenApiMediaType
                        {
                            Schema = new OpenApiSchemaReference(schemaNameWithHttpMethod + "_Response")
                            {
                                ReadOnly = true
                            }
                        }
                    }
                }
            };
            postOperation.Responses!.Add("201", postResponse);

            AddOperationToPath(openApiDoc, httpMethod, baseEndpointUri, endpointDescription, postOperation);
        }

        // WithId or WithTimestamp or WithCode operations
        if (httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Put || httpMethod == HttpMethod.Delete)
        {
            var withIdOperation = CreateDefaultOperation(httpMethod, baseSchemaName, baseEndpointUri);

            var name = keyName?.ToLowerInvariant() ?? (isSyncInterface ? "timestamp" : "id");
            var type = (keyType == null || name == "timestamp") ? new OpenApiSchema { Type = JsonSchemaType.String } : keyType;
            //type.Description = null;

            if (name == "hid")
            {
                name = "id"; // Normalize 'hid' to 'id' for consistency
            }

            string description;
            if (name == "timestamp")
            {
                description = $"The Timestamp of the {baseSchemaName}";
            }
            else
            {
                var format = !string.IsNullOrEmpty(type.Format) ? $" ({type.Format})" : string.Empty;
                description = $"Unique identifier{format} of the {baseSchemaName}";
            }

            var idParameter = new OpenApiParameter
            {
                Name = name,
                In = ParameterLocation.Path,
                Required = true,
                Schema = type,
                Description = description
            };
            var append = $"({{{name}}})";

            withIdOperation.Parameters!.Add(idParameter);

            if (httpMethod == HttpMethod.Get)
            {
                var schemaRef = new OpenApiSchemaReference(baseSchemaName)
                {
                    ReadOnly = true
                };

                var getResponseSchema = new OpenApiSchema
                {
                    Description = GetResponseDescription(baseSchemaName, false),
                    Type = JsonSchemaType.Object,
                    ReadOnly = true,
                    Properties = new Dictionary<string, IOpenApiSchema>
                    {
                        { "d", schemaRef }
                    },
                    Required = new HashSet<string> { "d" }
                };
                openApiDoc.Components!.Schemas!.Add(schemaNameWithHttpMethod + "_Response", getResponseSchema);

                var responseRef = new OpenApiSchemaReference(schemaNameWithHttpMethod + "_Response")
                {
                    ReadOnly = true
                };

                var getResponse = new OpenApiResponse
                {
                    Description = $"{httpMethod} operation successful",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        { MediaTypeNames.Application.Json, new OpenApiMediaType { Schema = responseRef } }
                    }
                };
                withIdOperation.Responses!.Add("200", getResponse);
            }
            else if (httpMethod == HttpMethod.Put)
            {
                var schemaRef = new OpenApiSchemaReference(schemaNameWithHttpMethod)
                {
                    ReadOnly = true
                };

                withIdOperation.RequestBody = new OpenApiRequestBody
                {
                    Description = $"The {baseSchemaName} entity to update.",
                    Required = true,
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        { MediaTypeNames.Application.Json, new OpenApiMediaType { Schema = schemaRef } }
                    }
                };

                withIdOperation.Responses!.Add("204", new OpenApiResponse
                {
                    Description = $"{httpMethod} operation successful"
                });
            }
            else
            {
                withIdOperation.Responses!.Add("204", new OpenApiResponse
                {
                    Description = $"{httpMethod} operation successful"
                });
            }

            AddOperationToPath(openApiDoc, httpMethod, baseEndpointUri + append, endpointDescription, withIdOperation);
        }
    }

    private static void AddOperationToPath(OpenApiDocument openApiDoc, HttpMethod httpMethod, string endpointUri, string endpointDescription, OpenApiOperation operation)
    {
        if (openApiDoc.Paths.TryGetValue(endpointUri, out var existingPath))
        {
            existingPath.Operations!.Add(httpMethod, operation);
        }
        else
        {
            var pathItem = new OpenApiPathItem
            {
                Description = endpointDescription,
                Operations = new Dictionary<HttpMethod, OpenApiOperation>
                {
                    { httpMethod, operation }
                }
            };
            openApiDoc.Paths.Add(endpointUri, pathItem);
        }
    }


    private static void AddODataQueryParameters(IList<IOpenApiParameter> parameters, bool isSyncInterface)
    {
        if (parameters.All(p => p.Name != "$filter"))
        {
            parameters.Add(new OpenApiParameter
            {
                Name = "$filter",
                In = ParameterLocation.Query,
                Required = isSyncInterface,
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.String
                },
                Description = isSyncInterface ? "OData filter, e.g., `Timestamp gt 5`" : "OData filter, e.g., `ID eq guid'00000000-0000-0000-0000-000000000000'`"
            });
        }

        if (parameters.All(p => p.Name != "$select"))
        {
            parameters.Add(new OpenApiParameter
            {
                Name = "$select",
                In = ParameterLocation.Query,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.String
                },
                Description = "Comma-separated list of fields to return, e.g., `ID`"
            });
        }

        if (parameters.All(p => p.Name != "$top"))
        {
            parameters.Add(new OpenApiParameter
            {
                Name = "$top",
                In = ParameterLocation.Query,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.Integer
                },
                Description = "Number of records to return, e.g., `100`"
            });
        }

        if (parameters.All(p => p.Name != "$skip"))
        {
            parameters.Add(new OpenApiParameter
            {
                Name = "$skip",
                In = ParameterLocation.Query,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.Integer
                },
                Description = "Number of records to skip, e.g., `10`"
            });
        }

        if (parameters.All(p => p.Name != "$skiptoken"))
        {
            parameters.Add(new OpenApiParameter
            {
                Name = "$skiptoken",
                In = ParameterLocation.Query,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.String
                },
                Description = "A server-generated token used to fetch the next page of results in a paginated query."
            });
        }

        if (parameters.All(p => p.Name != "$orderby"))
        {
            parameters.Add(new OpenApiParameter
            {
                Name = "$orderby",
                In = ParameterLocation.Query,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.String
                },
                Description = "Order by field, e.g., `ID desc`"
            });
        }

        if (parameters.All(p => p.Name != "$count"))
        {
            parameters.Add(new OpenApiParameter
            {
                Name = "$count",
                In = ParameterLocation.Query,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.Boolean
                },
                Description = "Include count of items, e.g., `true`"
            });
        }

        if (parameters.All(p => p.Name != "$inlinecount"))
        {
            parameters.Add(new OpenApiParameter
            {
                Name = "$inlinecount",
                In = ParameterLocation.Query,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.String
                },
                Description = "Include inline count, e.g., `allpages`"
            });
        }

        if (parameters.All(p => p.Name != "$expand"))
        {
            parameters.Add(new OpenApiParameter
            {
                Name = "$expand",
                In = ParameterLocation.Query,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.String
                },
                Description = "Expand related entities, e.g., `ParentEntity`"
            });
        }
    }

    private static (string ServiceUri, HashSet<OpenApiParameter> QueryParameters) GetEndpointUriDetails(HtmlDocument doc)
    {
        var parameters = new HashSet<OpenApiParameter>();
        var serviceUriNode = doc.DocumentNode.SelectSingleNode("//p[@id='serviceUri']");

        // Use regex to find parameter patterns like: paramName={EdmType}
        var matches = EndpointUriEdmTypeRegex.Matches(serviceUriNode.InnerText);

        // Get all strong tags to determine which parameters are required
        var strongNodes = serviceUriNode.SelectNodes(".//strong");
        var requiredParamNames = new HashSet<string>();

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (strongNodes != null)
        {
            foreach (var strongNode in strongNodes)
            {
                var paramName = strongNode.InnerText.Trim();
                if (!string.IsNullOrEmpty(paramName))
                {
                    requiredParamNames.Add(paramName);
                }
            }
        }

        foreach (Match match in matches)
        {
            var paramName = match.Groups[1].Value.Trim();
            var edmType = match.Groups[2].Value.Trim();

            if (string.IsNullOrEmpty(paramName))
            {
                continue;
            }

            var parameter = new OpenApiParameter
            {
                Name = paramName,
                In = ParameterLocation.Query,
                Required = requiredParamNames.Contains(paramName),
                Schema = GetQueryParameterScheme(edmType)
            };

            parameters.Add(parameter);
        }

        var serviceUri = serviceUriNode.InnerText.Trim();
        return (serviceUri.Contains('?') ? serviceUri.Split('?')[0] : serviceUri, parameters);
    }

    private static IOpenApiSchema GetQueryParameterScheme(string edmType)
    {
        var fixedStringType = new HashSet<Type>
        {
            typeof(DateTimeOffset),
            typeof(Guid)
        };

        if (EdmTypeParser.TryParse(edmType, description: null, isSyncInterface: false, out (Type Type, IOpenApiSchema Schema) typeWithSchema) && !fixedStringType.Contains(typeWithSchema.Type))
        {
            return typeWithSchema.Schema;
        }

        return new OpenApiSchema
        {
            Type = JsonSchemaType.String
        };
    }
}
using System.Text.RegularExpressions;
using ExactOnline.OpenApiGenerator.Extensions;
using ExactOnline.OpenApiGenerator.HtmlDocumentLoaders;
using ExactOnline.OpenApiGenerator.Parsers;
using HtmlAgilityPack;
using Microsoft.OpenApi;
using MonkeyCache.FileStore;

namespace ExactOnline.OpenApiGenerator.Crawlers;

internal class EndpointCrawler
{
    private const int MaxRetries = 3;
    private static readonly Regex EndpointUriRegex = new(@"\{(\w+)\}", RegexOptions.Compiled);
    private static readonly Regex EndpointUriEdmTypeRegex = new(@"(\w+)=\{([^}]+)\}", RegexOptions.Compiled);

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
                    Properties = new Dictionary<string, IOpenApiSchema>
                    {
                        ["code"] = new OpenApiSchema
                        {
                            Type = JsonSchemaType.String,
                            Description = "Service-defined error code"
                        },
                        ["message"] = new OpenApiSchema
                        {
                            Type = JsonSchemaType.Object,
                            Properties = new Dictionary<string, IOpenApiSchema>
                            {
                                ["lang"] = new OpenApiSchema
                                {
                                    Type = JsonSchemaType.String,
                                    Description = "Language code (e.g., en-us)"
                                },
                                ["value"] = new OpenApiSchema
                                {
                                    Type = JsonSchemaType.String,
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
        if (!Barrel.Current.IsExpired(key: url))
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

    private static void Process(string pageUrl, IDictionary<HttpMethod, HtmlDocument> docs, OpenApiDocument openApiDoc)
    {
        var docGet = docs[HttpMethod.Get];

        var baseSchemaName = pageUrl.Split("?name=").Last().Trim();
        var endpointDescription = docGet.DocumentNode.SelectSingleNode("//p[@id='goodToKnow']")?.InnerText.Trim() ?? string.Empty;
        var schemaIsCollection = IsCollection(baseSchemaName, endpointDescription);
        var responseDescription = schemaIsCollection ? $"A collection of {baseSchemaName} entities." : $"The {baseSchemaName} entity.";
        var (baseEndpointUri, queryParameters) = GetEndpointUriDetails(docGet);
        var isSyncInterface = baseSchemaName.StartsWith("Sync");

        foreach (var (httpMethod, document) in docs)
        {
            var properties = new Dictionary<string, IOpenApiSchema>
            {
                { "__metadata", new OpenApiSchemaReference("ExactOnlineMetadata") }
            };

            var requiredProperties = new HashSet<string>();

            // Rows from the tabel which:
            // - are not headers (no class='header')
            // - are not hidden (no style='display: none')
            var propertyRows = document.DocumentNode.SelectNodes("//table[@id='referencetable']//tr[not(@class='header') and not(contains(@style, 'display: none'))]");
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (propertyRows != null)
            {
                foreach (var row in propertyRows)
                {
                    var rowIsKey = row.GetClasses().Contains("key");

                    var columns = row.SelectNodes("td");
                    if (columns is { Count: >= 7 })
                    {
                        var nameColumn = columns[1];
                        var name = nameColumn.InnerText.Trim();
                        var linkNode = nameColumn.SelectSingleNode(".//a");
                        var href = linkNode?.Attributes["href"]?.Value;
                        var linkedSchemaName = href?.Split("?name=").Last().Trim();

                        var type = columns[5].InnerText.Trim().Split(' ')[0].Trim();
                        var description = columns[6].InnerText.Trim();
                        if (string.IsNullOrEmpty(description) && columns.Count >= 9)
                        {
                            description = columns[8].InnerText.Trim();
                        }
                        var isCollection = description.Contains("collection of", StringComparison.OrdinalIgnoreCase);
                        var isRequired = rowIsKey || bool.TryParse(columns[2].InnerText.Trim(), out var isMandatoryValue) && isMandatoryValue;

                        if (string.IsNullOrEmpty(name))
                        {
                            continue;
                        }

                        if (!EdmTypeParser.TryParse(type, description, out var property))
                        {
                            if (!string.IsNullOrEmpty(linkedSchemaName))
                            {
                                if (isCollection)
                                {
                                    property = new OpenApiSchema
                                    {
                                        Type = JsonSchemaType.Array,
                                        Description = description,
                                        Items = new OpenApiSchemaReference(linkedSchemaName)
                                    };
                                }
                                else
                                {
                                    property = new OpenApiSchemaReference(linkedSchemaName);
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
                                        }
                                    };
                                }
                                else
                                {
                                    property = new OpenApiSchema
                                    {
                                        Type = JsonSchemaType.Object,
                                        Description = description
                                    };
                                }
                            }
                        }

                        if (isRequired)
                        {
                            requiredProperties.Add(name);
                        }

                        properties.Add(name, property);
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

            if (httpMethod == HttpMethod.Get)
            {
                var array = new OpenApiSchema
                {
                    Type = JsonSchemaType.Array,
                    Items = new OpenApiSchemaReference(schemaName)
                };
                openApiDoc.Components!.Schemas!.Add(schemaName + "_Array", array);

                var arrayReference = new OpenApiSchemaReference(schemaName + "_Array");

                var results = new OpenApiSchema
                {
                    Type = JsonSchemaType.Object,
                    Properties = new Dictionary<string, IOpenApiSchema>
                    {
                        { "results", arrayReference },
                        { "__next", new OpenApiSchema { Type = JsonSchemaType.String } }
                    },
                    Required = new HashSet<string> { "results" }
                };
                openApiDoc.Components!.Schemas!.Add(schemaName + "_Results", results);

                var resultsReference = new OpenApiSchemaReference(schemaName + "_Results");

                var response = new OpenApiSchema
                {
                    Description = responseDescription,
                    Type = JsonSchemaType.Object,
                    Properties = new Dictionary<string, IOpenApiSchema>
                    {
                        { "d", new OpenApiSchema
                            {
                                OneOf =
                                [
                                    arrayReference,
                                    resultsReference
                                ],
                                Discriminator = new OpenApiDiscriminator
                                {
                                    PropertyName = "results",
                                    Mapping = new Dictionary<string, OpenApiSchemaReference>
                                    {
                                        { "_Results", resultsReference }
                                    }
                                }
                            }
                        }
                    },
                    Required = new HashSet<string> { "d" }
                };
                openApiDoc.Components!.Schemas!.Add(schemaName + "_Response", response);
            }
            else if (httpMethod == HttpMethod.Post)
            {
                var response = new OpenApiSchema
                {
                    Description = responseDescription,
                    Type = JsonSchemaType.Object,
                    Properties = new Dictionary<string, IOpenApiSchema>
                    {
                        { "d", new OpenApiSchemaReference(baseSchemaName) }
                    },
                    Required = new HashSet<string> { "d" }
                };
                openApiDoc.Components!.Schemas!.Add(schemaName + "_Response", response);
            }

            var endpointUri = baseEndpointUri;

            var operation = new OpenApiOperation
            {
                Summary = $"{httpMethod} {baseSchemaName}",
                Parameters = new List<IOpenApiParameter>(),
                Responses = new OpenApiResponses()
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

            if (httpMethod == HttpMethod.Get)
            {
                foreach (var queryParameter in queryParameters)
                {
                    operation.Parameters.Add(queryParameter);
                }

                AddODataQueryParameters(operation.Parameters, isSyncInterface);
            }

            if (httpMethod == HttpMethod.Put || httpMethod == HttpMethod.Delete)
            {
                endpointUri = baseEndpointUri + "({id})";

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "id",
                    In = ParameterLocation.Path,
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = JsonSchemaType.String,
                        Format = "uuid"
                    },
                    Description = $"Unique identifier (GUID) of the {baseSchemaName}"
                });
            }

            if (httpMethod == HttpMethod.Put || httpMethod == HttpMethod.Post)
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Description = $"The {baseSchemaName} entity to create or update.",
                    Required = true,
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        {
                            "application/json", new OpenApiMediaType
                            {
                                Schema = new OpenApiSchemaReference(schemaName)
                            }
                        }
                    }
                };
            }

            if (httpMethod == HttpMethod.Get)
            {
                operation.Responses.Add("200", new OpenApiResponse
                {
                    Description = responseDescription,
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        {
                            "application/json", new OpenApiMediaType
                            {
                                Schema = new OpenApiSchemaReference(schemaName + "_Response")
                            }
                        }
                    }
                });
            }
            else if (httpMethod == HttpMethod.Post)
            {
                operation.Responses.Add("201", new OpenApiResponse
                {
                    Description = $"{httpMethod} operation successful",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        {
                            "application/json", new OpenApiMediaType
                            {
                                Schema = new OpenApiSchemaReference(schemaName + "_Response")
                            }
                        }
                    }
                });
            }
            else
            {
                // For Put and Delete operations, use 204.
                operation.Responses.Add("204", new OpenApiResponse
                {
                    Description = $"{httpMethod} operation successful"
                });
            }

            operation.Responses.Add("400", new OpenApiResponse
            {
                Description = $"{httpMethod} operation failed",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    {
                        "application/json", new OpenApiMediaType
                        {
                            Schema = new OpenApiSchemaReference("ODataError")
                        }
                    }
                }
            });

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

        if (isSyncInterface)
        {
            if (parameters.All(p => p.Name != "$skiptoken"))
            {
                parameters.Add(new OpenApiParameter
                {
                    Name = "$skiptoken",
                    In = ParameterLocation.Query,
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = JsonSchemaType.Integer,
                        Format = "int64"
                    },
                    Description = "Number of records to skip, e.g., `10`"
                });
            }
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

    private static bool IsCollection(string schemaName, string endpointDescription)
    {
        return schemaName.EndsWith("s") ||
               schemaName.EndsWith("List") ||
               endpointDescription.Contains("returns a list");
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

            EdmTypeParser.TryParse(edmType, null, out var schema);

            //var description = paramName switch
            //{
            //    "$filter" => "OData filter, e.g., `ID eq guid'00000000-0000-0000-0000-000000000000'`",
            //    "$select" => "Comma-separated list of fields to return, e.g., `ID`",
            //    "$top" => "Number of records to return, e.g., `100`",
            //    "$skiptoken" => "Number of records to skip, e.g., `10`",
            //    "$orderby" => "Order by field, e.g., `ID desc`",
            //    "$count" => "Include count of items, e.g., `true`",
            //    "$inlinecount" => "Include inline count, e.g., `allpages`",
            //    "$expand" => "Expand related entities, e.g., `ParentEntity`",
            //    _ => $"Query parameter of type {edmType}"
            //};

            var parameter = new OpenApiParameter
            {
                Name = paramName,
                In = ParameterLocation.Query,
                Required = requiredParamNames.Contains(paramName),
                Schema = new OpenApiSchema
                {
                    Type = schema?.Type ?? JsonSchemaType.String
                },
                // Description = description
            };

            parameters.Add(parameter);
        }

        var serviceUri = serviceUriNode.InnerText.Trim();
        return (serviceUri.Contains('?') ? serviceUri.Split('?')[0] : serviceUri, parameters);
    }
}
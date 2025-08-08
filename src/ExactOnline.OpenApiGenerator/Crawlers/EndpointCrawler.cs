using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using ExactOnline.OpenApiGenerator.HtmlDocumentLoaders;
using HtmlAgilityPack;
using Microsoft.OpenApi;

namespace ExactOnline.OpenApiGenerator.Crawlers;

internal class EndpointCrawler
{
    private const int MaxRetries = 3;
    private static readonly Regex EndpointUriRegex = new(@"\{(\w+)\}", RegexOptions.Compiled);
    private static readonly Regex EndpointUriEdmTypeRegex = new(@"(\w+)=\{([^}]+)\}", RegexOptions.Compiled);

    private readonly OpenApiDocument _openApiDoc;
    private readonly IReadOnlyList<string> _urls;

    internal EndpointCrawler(IReadOnlyList<string> urls)
    {
        _urls = urls;

        var metadata = new OpenApiSchema
        {
            Type = JsonSchemaType.Object,
            Properties = new Dictionary<string, IOpenApiSchema>
            {
                { "uri", new OpenApiSchema { Type = JsonSchemaType.String } },
                { "type", new OpenApiSchema { Type = JsonSchemaType.String } }
            }
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
                    { "ExactOnlineMetadata", metadata }
                }
            }
        };
    }

    internal async Task<OpenApiDocument> CrawlAsync(Action<string> onEndpointCrawling, CancellationToken cancellationToken)
    {
        await using var htmlLoader = new PuppeteerHtmlDocumentLoader();

        foreach (var url in _urls)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            onEndpointCrawling(url);

            var retries = 0;
            while (retries < MaxRetries)
            {
                try
                {
                    var doc = await htmlLoader.LoadAsync(url, cancellationToken);
                    Process(url, doc, _openApiDoc);
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

                retries++;
            }
        }

        return _openApiDoc;
    }

    private static void Process(string pageUrl, HtmlDocument doc, OpenApiDocument openApiDoc)
    {
        var schemaName = pageUrl.Split("?name=").Last().Trim();
        var endpointDescription = doc.DocumentNode.SelectSingleNode("//p[@id='goodToKnow']")?.InnerText.Trim() ?? string.Empty;
        var schemaIsCollection = IsCollection(schemaName, endpointDescription);
        var responseDescription = schemaIsCollection ? $"A collection of {schemaName} entities." : $"The {schemaName} entity.";
        var (baseEndpointUri, queryParameters) = GetEndpointUriDetails(doc);

        var methods = doc.DocumentNode
            .SelectNodes("//input[@name='supportedmethods']")
            .Select(n => HttpMethod.Parse(n.Attributes["value"].Value))
            .ToArray();

        var properties = new Dictionary<string, IOpenApiSchema>
        {
            { "__metadata", new OpenApiSchemaReference("ExactOnlineMetadata") }
        };

        var requiredProperties = new HashSet<string>();

        var propertyRows = doc.DocumentNode.SelectNodes("//table[@id='referencetable']//tr[not(@class='header')]");
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

                    if (!TryParseAsEdmType(type, description, out var property))
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

        var result = new OpenApiSchema
        {
            Type = JsonSchemaType.Object,
            Properties = properties,
            Required = requiredProperties
        };
        openApiDoc.Components!.Schemas!.Add(schemaName, result);

        var results = new OpenApiSchema
        {
            Type = JsonSchemaType.Object,
            Properties = new Dictionary<string, IOpenApiSchema>
            {
                { "results", new OpenApiSchema
                    {
                        Type = JsonSchemaType.Array,
                        Items = new OpenApiSchemaReference(schemaName)
                    }
                }
            },
            Required = new HashSet<string> { "results" }

        };

        var response = new OpenApiSchema
        {
            Description = responseDescription,
            Type = JsonSchemaType.Object,
            Properties = new Dictionary<string, IOpenApiSchema>
            {
                { "d", results }
            },
            Required = new HashSet<string> { "d" }
        };
        openApiDoc.Components!.Schemas!.Add(schemaName + "_Response", response);

        foreach (var method in methods)
        {
            var endpointUri = baseEndpointUri;

            var operation = new OpenApiOperation
            {
                Summary = $"{method} {schemaName}",
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

            if (method == HttpMethod.Get)
            {
                foreach (var queryParameter in queryParameters)
                {
                    operation.Parameters.Add(queryParameter);
                }

                AddODataQueryParameters(operation.Parameters);
            }

            if (method == HttpMethod.Put || method == HttpMethod.Delete)
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
                    Description = $"Unique identifier (GUID) of the {schemaName}"
                });
            }

            if (method == HttpMethod.Put || method == HttpMethod.Post)
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Description = $"The {schemaName} entity to create or update.",
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

            if (method == HttpMethod.Get)
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
            else
            {
                operation.Responses.Add("200", new OpenApiResponse
                {
                    Description = $"{method} operation successful"
                });
            }

            if (openApiDoc.Paths.TryGetValue(endpointUri, out var existingPath))
            {
                existingPath.Operations!.Add(method, operation);
            }
            else
            {
                var pathItem = new OpenApiPathItem
                {
                    Description = endpointDescription,
                    Operations = new Dictionary<HttpMethod, OpenApiOperation>
                    {
                        { method, operation }
                    }
                };
                openApiDoc.Paths.Add(endpointUri, pathItem);
            }
        }
    }

    private static void AddODataQueryParameters(IList<IOpenApiParameter> parameters)
    {
        if (parameters.All(p => p.Name != "$filter"))
        {
            parameters.Add(new OpenApiParameter
            {
                Name = "$filter",
                In = ParameterLocation.Query,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.String
                },
                Description = "OData filter, e.g., `ID eq guid'00000000-0000-0000-0000-000000000000'`"
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

            TryParseAsEdmType(edmType, null, out var schema);

            var description = paramName switch
            {
                "$filter" => "OData filter, e.g., `ID eq guid'00000000-0000-0000-0000-000000000000'`",
                "$select" => "Comma-separated list of fields to return, e.g., `ID`",
                _ => $"Query parameter of type {edmType}"
            };

            var parameter = new OpenApiParameter
            {
                Name = paramName,
                In = ParameterLocation.Query,
                Required = requiredParamNames.Contains(paramName),
                Schema = new OpenApiSchema
                {
                    Type = schema?.Type ?? JsonSchemaType.String
                },
                Description = description
            };

            parameters.Add(parameter);
        }

        var serviceUri = serviceUriNode.InnerText.Trim();
        return (serviceUri.Contains('?') ? serviceUri.Split('?')[0] : serviceUri, parameters);
    }

    private static bool TryParseAsEdmType(string type, string? description, [NotNullWhen(true)] out IOpenApiSchema? schema)
    {
        var property = new OpenApiSchema
        {
            Description = description
        };

        switch (type)
        {
            case "Edm.Binary":
                property.Type = JsonSchemaType.String;
                property.Format = "byte";
                schema = property;
                return true;

            case "Edm.Byte":
                property.Type = JsonSchemaType.Integer;
                property.Format = "int32";
                property.Minimum = "0";
                property.Maximum = "255";
                schema = property;
                return true;

            case "Edm.Boolean":
                property.Type = JsonSchemaType.Boolean;
                schema = property;
                return true;

            case "Edm.DateTime":
                property.Type = JsonSchemaType.String;
                property.Format = "date-time";
                schema = property;
                return true;

            case "Edm.Decimal":
                property.Type = JsonSchemaType.Number;
                property.Format = "decimal";
                schema = property;
                return true;

            case "Edm.Double":
                property.Type = JsonSchemaType.Number;
                property.Format = "double";
                schema = property;
                return true;

            case "Edm.Float":
                property.Type = JsonSchemaType.Number;
                property.Format = "float";
                schema = property;
                return true;

            case "Edm.Guid":
                property.Type = JsonSchemaType.String;
                property.Format = "uuid";
                schema = property;
                return true;

            case "Edm.Int16":
                property.Type = JsonSchemaType.Integer;
                property.Format = "int16";
                schema = property;
                return true;

            case "Edm.Int32":
                property.Type = JsonSchemaType.Integer;
                property.Format = "int32";
                schema = property;
                return true;

            case "Edm.Int64":
                property.Type = JsonSchemaType.Integer;
                property.Format = "int64";
                schema = property;
                return true;

            case "Edm.String":
                property.Type = JsonSchemaType.String;
                schema = property;
                return true;

            default:
                schema = null;
                return false;
        }
    }
}
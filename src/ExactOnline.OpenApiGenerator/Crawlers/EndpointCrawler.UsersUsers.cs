using ExactOnline.OpenApiGenerator.Parsers;
using Microsoft.OpenApi;

namespace ExactOnline.OpenApiGenerator.Crawlers;

internal partial class EndpointCrawler
{
    private static void AddUsersUsers(OpenApiDocument openApiDoc)
    {
        var schemaName = "UsersUsers";
        var description = "Get all Users";
        var baseEndpointUri = "/api/v1/{division}/users/Users";
        var keyName = "UserID";
        var keyType = new OpenApiSchema
        {
            Type = JsonSchemaType.String,
            Format = "uuid"
        };
        var queryParameters = new HashSet<OpenApiParameter>();

        var propertyMap = new (string Name, string Type)[]
        {
            ("BirthDate", "Edm.DateTime"),
            ("BirthName", "Edm.String"),
            ("Created", "Edm.DateTime"),
            ("Creator", "Edm.Guid"),
            ("CreatorFullName", "Edm.String"),
            ("Customer", "Edm.Guid"),
            ("CustomerName", "Edm.String"),
            ("Email", "Edm.String"),
            ("EndDate", "Edm.DateTime"),
            ("FirstName", "Edm.String"),
            ("FullName", "Edm.String"),
            ("Gender", "Edm.String"),
            ("HasRegisteredForTwoStepVerification", "Edm.Boolean"),
            ("HasTwoStepVerification", "Edm.Boolean"),
            ("Initials", "Edm.String"),
            ("IsAnonymised", "Edm.Byte"),
            ("Language", "Edm.String"),
            ("LastLogin", "Edm.DateTime"),
            ("LastName", "Edm.String"),
            ("MiddleName", "Edm.String"),
            ("Mobile", "Edm.String"),
            ("Modified", "Edm.DateTime"),
            ("Modifier", "Edm.Guid"),
            ("ModifierFullName", "Edm.String"),
            ("Nationality", "Edm.String"),
            ("Notes", "Edm.String"),
            ("Phone", "Edm.String"),
            ("PhoneExtension", "Edm.String"),
            ("ProfileCode", "Edm.String"),
            ("StartDate", "Edm.DateTime"),
            ("StartDivision", "Edm.Int32"),
            ("Title", "Edm.String"),
            ("UserID", "Edm.Guid"),
            ("UserName", "Edm.String"),
            ("UserTypeCode", "Edm.String"),
            ("UserTypesList", "Edm.String")
        };

        var properties = new Dictionary<string, IOpenApiSchema>
        {
            { "__metadata", MetaDataRef },
            { "UserRoles", ODataDeferredRef },
            { "UserRolesPerDivision", ODataDeferredRef }
        };

        foreach (var entry in propertyMap)
        {
            if (EdmTypeParser.TryParse(entry.Type, description: string.Empty, isGetOnly: true, out IOpenApiSchema? property))
            {
                properties.Add(entry.Name, property);
            }
        }

        var requiredProperties = new HashSet<string>()
        {
            keyName
        };

        var entityComponent = new OpenApiSchema
        {
            Type = JsonSchemaType.Object,
            Properties = properties,
            Required = requiredProperties
        };

        openApiDoc.Components!.Schemas!.Add(schemaName, entityComponent);

        AddOperations(openApiDoc, HttpMethod.Get, baseEndpointUri, schemaName, description, queryParameters, keyName, keyType, isSyncInterface: false, isSingleResponse: false);
    }
}

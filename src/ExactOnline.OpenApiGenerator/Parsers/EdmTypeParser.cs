using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi;

namespace ExactOnline.OpenApiGenerator.Parsers;

internal static class EdmTypeParser
{
    internal static bool TryParse(string type, string? description, [NotNullWhen(true)] out IOpenApiSchema? schema)
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
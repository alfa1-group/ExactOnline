using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi;

namespace ExactOnline.OpenApiGenerator.Parsers;

internal static class EdmTypeParser
{
    internal static bool TryParse(string type, string? description, bool isSyncInterface, [NotNullWhen(true)] out IOpenApiSchema? schema)
    {
        if (TryParse(type, description, isSyncInterface, out (Type Type, IOpenApiSchema Schema) typeWithSchema))
        {
            schema = typeWithSchema.Schema;
            return true;
        }

        schema = null;
        return false;
    }

    internal static bool TryParse(string type, string? description, bool isSyncInterface, out (Type Type, IOpenApiSchema Schema) typeWithSchema)
    {
        var edmSchema = new OpenApiSchema
        {
            Description = description,
            ReadOnly = isSyncInterface
        };

        switch (type)
        {
            case "Edm.Binary":
                edmSchema.Type = JsonSchemaType.String;
                edmSchema.Format = "byte";
                typeWithSchema = (typeof(byte[]), edmSchema);
                return true;

            case "Edm.Byte":
                edmSchema.Type = JsonSchemaType.Integer;
                edmSchema.Format = "int32";
                edmSchema.Minimum = "0";
                edmSchema.Maximum = "255";
                typeWithSchema = (typeof(byte), edmSchema);
                return true;

            case "Edm.Boolean":
                edmSchema.Type = JsonSchemaType.Boolean;
                typeWithSchema = (typeof(bool), edmSchema);
                return true;

            case "Edm.DateTime":
                edmSchema.Type = JsonSchemaType.String;
                edmSchema.Format = "date-time";
                typeWithSchema = (typeof(DateTimeOffset), edmSchema);
                return true;

            case "Edm.Decimal":
                edmSchema.Type = JsonSchemaType.Number;
                edmSchema.Format = "decimal";
                typeWithSchema = (typeof(decimal), edmSchema);
                return true;

            case "Edm.Double":
                edmSchema.Type = JsonSchemaType.Number;
                edmSchema.Format = "double";
                typeWithSchema = (typeof(double), edmSchema);
                return true;

            case "Edm.Float":
                edmSchema.Type = JsonSchemaType.Number;
                edmSchema.Format = "float";
                typeWithSchema = (typeof(float), edmSchema);
                return true;

            case "Edm.Guid":
                edmSchema.Type = JsonSchemaType.String;
                edmSchema.Format = "uuid";
                typeWithSchema = (typeof(Guid), edmSchema);
                return true;

            case "Edm.Int16":
                edmSchema.Type = JsonSchemaType.Integer;
                edmSchema.Format = "int16";
                typeWithSchema = (typeof(short), edmSchema);
                return true;

            case "Edm.Int32":
                edmSchema.Type = JsonSchemaType.Integer;
                edmSchema.Format = "int32";
                typeWithSchema = (typeof(int), edmSchema);
                return true;

            case "Edm.Int64":
                edmSchema.Type = JsonSchemaType.Integer;
                edmSchema.Format = "int64";
                typeWithSchema = (typeof(long), edmSchema);
                return true;

            case "Edm.String":
                edmSchema.Type = JsonSchemaType.String;
                typeWithSchema = (typeof(string), edmSchema);
                return true;

            default:
                typeWithSchema = default;
                return false;
        }
    }
}
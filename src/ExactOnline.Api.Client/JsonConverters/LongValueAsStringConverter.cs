using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using ExactOnline.Api.Client.Utils;

namespace ExactOnline.Api.Client.JsonConverters;

public class LongValueAsStringConverter : JsonConverter<long>
{
    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && LongValueParser.TryParse(reader.GetString(), out var valueAsLong))
        {
            return valueAsLong;
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetInt64();
        }

        // For a non-nullable long, throw an exception if the token
        // is null or not a convertible type.
        throw new JsonException($"Unable to convert token type {reader.TokenType} to a long.");
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
    }
}
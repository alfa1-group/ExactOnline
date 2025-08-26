using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ExactOnline.Api.Client.JsonConverters;

/// <summary>
/// A custom JsonConverter to handle the Microsoft JSON Date format: "/Date(milliseconds)/".
/// This converter is necessary because System.Text.Json does not support this format by default.
/// </summary>
internal class LegacyDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    // Regex to extract the millisecond value from the date string.
    internal static readonly Regex MicrosoftDateRegex = new(@"^\/Date\((-?\d+)\)\/$", RegexOptions.Compiled);

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(DateTimeOffset);
    }

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected a string for the DateTimeOffset value.");
        }

        var dateString = reader.GetString() ?? throw new JsonException("DateTimeOffset string cannot be null.");
        var match = MicrosoftDateRegex.Match(dateString);

        if (match.Success && long.TryParse(match.Groups[1].Value, out var milliseconds))
        {
            // The epoch starts at 1970-01-01T00:00:00Z.
            // We use DateTimeOffset.FromUnixTimeMilliseconds for direct conversion.
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
        }

        // Fallback to standard ISO 8601 parsing if the custom format isn't matched.
        return DateTimeOffset.Parse(dateString, null, DateTimeStyles.RoundtripKind);
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        // Convert the DateTimeOffset to milliseconds since the Unix epoch.
        var milliseconds = value.ToUnixTimeMilliseconds();

        // Write the custom Microsoft JSON Date format.
        writer.WriteStringValue($"/Date({milliseconds})/");
    }
}
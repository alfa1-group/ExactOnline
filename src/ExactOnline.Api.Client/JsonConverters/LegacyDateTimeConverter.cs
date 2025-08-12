using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ExactOnline.Api.Client.JsonConverters;

/// <summary>
/// A custom JsonConverter to handle the Microsoft JSON Date format: "/Date(milliseconds)/" for DateTime.
/// This converter is necessary because System.Text.Json does not support this format by default.
/// </summary>
internal class LegacyDateTimeConverter : JsonConverter<DateTime>
{
    // Regex to extract the millisecond value from the date string.
    private static readonly Regex MicrosoftDateRegex = new(@"^\/Date\((\d+)\)\/$", RegexOptions.Compiled);

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(DateTime);
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected a string for the DateTime value.");
        }

        var dateString = reader.GetString() ?? throw new JsonException("DateTime string cannot be null.");
        var match = MicrosoftDateRegex.Match(dateString);

        if (match.Success && long.TryParse(match.Groups[1].Value, out var milliseconds))
        {
            // The epoch starts at 1970-01-01T00:00:00Z.
            // We use DateTimeOffset.FromUnixTimeMilliseconds for direct conversion.
            // The result is a UTC DateTime.
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).UtcDateTime;
        }

        // Fallback to standard ISO 8601 parsing if the custom format isn't matched.
        // This makes the converter more robust.
        return DateTime.Parse(dateString, null, DateTimeStyles.RoundtripKind);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        // To ensure consistency, treat the DateTime as UTC if its kind is unspecified.
        // This aligns with how Unix timestamps are defined (seconds since epoch in UTC).
        var valueAsUtc = value.Kind == DateTimeKind.Unspecified ? new DateTime(value.Ticks, DateTimeKind.Utc) : value.ToUniversalTime();

        // Convert the DateTime to milliseconds since the Unix epoch.
        var milliseconds = new DateTimeOffset(valueAsUtc).ToUnixTimeMilliseconds();

        // Write the custom Microsoft JSON Date format.
        writer.WriteStringValue($"/Date({milliseconds})/");
    }
}
using System.Globalization;

namespace ExactOnline.Api.Client.Utils;

internal static class LongValueTransformer
{
    /// <summary>
    /// If the value exceeds int.MaxValue, append "L" to indicate it's a long.
    /// </summary>
    internal static string? ToString(long? value)
    {
        if (value == null)
        {
            return null;
        }

        return value > int.MaxValue ? $"{value}L" : value.Value.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Parses a string value to a long. If the string ends with "L", it is removed before parsing.
    /// </summary>
    internal static bool TryParse(string? value, out long valueAsLong)
    {
        if (value == null)
        {
            valueAsLong = default;
            return false;
        }

        if (value.EndsWith("L", StringComparison.OrdinalIgnoreCase))
        {
            value = value[..^1];
        }

        return long.TryParse(value, out valueAsLong);
    }
}
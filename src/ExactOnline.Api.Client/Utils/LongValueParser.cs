namespace ExactOnline.Api.Client.Utils;

public static class LongValueParser
{
    /// <summary>
    /// Parses a string value to a long. If the string ends with "L", it is removed before parsing.
    /// </summary>
    public static bool TryParse(string? str, out long value)
    {
        if (str == null)
        {
            value = default;
            return false;
        }

        if (str.EndsWith("L", StringComparison.OrdinalIgnoreCase))
        {
            str = str[..^1];
        }

        return long.TryParse(str, out value);
    }
}
using System.Globalization;

namespace ExactOnline.Api.Client.Utils;

internal static class LongValueTransformer
{
    /// <summary>
    /// If the value exceeds int.MaxValue, append "L" to indicate it's a long.
    /// </summary>
    internal static string Transform(long value)
    {
        return value > int.MaxValue ? $"{value}L" : value.ToString(CultureInfo.InvariantCulture);
    }
}
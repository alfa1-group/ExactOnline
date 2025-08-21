// ReSharper disable once CheckNamespace
namespace ExactOnline.Api.Client.Extensions;

public static class LongExtensions
{
    /// <summary>
    /// If the value exceeds int.MaxValue, append "L" to indicate it's a long.
    /// </summary>
    public static string ToODataFormat(this long value)
    {
        return value > int.MaxValue ? $"{value}L" : $"{value}";
    }
}
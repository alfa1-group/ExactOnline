using ExactOnline.Api.Client.Utils;

// ReSharper disable once CheckNamespace
namespace Microsoft.Kiota.Abstractions.Serialization;

internal static class ParseNodeExtensions
{
    internal static long? GetTimestampAsLongValue(this IParseNode parseNode)
    {
        var longValue = parseNode.GetLongValue();
        if (longValue.HasValue)
        {
            return longValue.Value;
        }

        var value = parseNode.GetStringValue();
        if (LongValueTransformer.TryParse(value, out var parsedValueAsLong))
        {
            return parsedValueAsLong;
        }

        return null;
    }
}
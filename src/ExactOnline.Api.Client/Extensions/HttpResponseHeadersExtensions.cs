// ReSharper disable once CheckNamespace
namespace System.Net.Http.Headers;

internal static class HttpResponseHeadersExtensions
{
    internal static bool TryGetFirstAsLong(this HttpResponseHeaders headers, string name, out long value)
    {
        if (headers.TryGetValues(name, out var values))
        {
            using var enumerator = values.GetEnumerator();
            if (enumerator.MoveNext())
            {
                var valueAsString = enumerator.Current;
                return long.TryParse(valueAsString, out value);
            }

            throw new InvalidOperationException($"{name} header is empty.");
        }

        value = default;
        return false;
    }
}
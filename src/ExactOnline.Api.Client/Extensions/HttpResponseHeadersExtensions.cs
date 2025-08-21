// ReSharper disable once CheckNamespace
namespace System.Net.Http.Headers;

internal static class HttpResponseHeadersExtensions
{
    internal static bool TryGetFirstValueAsLong(this HttpResponseHeaders headers, string headerName, out long value)
    {
        if (headers.TryGetValues(headerName, out var values))
        {
            using var enumerator = values.GetEnumerator();
            if (enumerator.MoveNext())
            {
                var valueAsString = enumerator.Current;
                return long.TryParse(valueAsString, out value);
            }

            throw new InvalidOperationException($"The {headerName} header is empty.");
        }

        value = default;
        return false;
    }
}
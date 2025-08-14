using System.Web;
using ExactOnline.Api.Client.Utils;

namespace ExactOnline.Api.Client.Middleware;

public class QueryParametersHandler : DelegatingHandler
{
    private static readonly Dictionary<string, Func<string?, string?>> Transformations = new()
    {
        // ExactOnline API requires that the $skiptoken has "L" appended if it exceeds int.MaxValue.
        ["$skiptoken"] = value => long.TryParse(value, out var longValue) ? LongValueTransformer.ToString(longValue) : value
    };

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        TransformQueryParameters(request);
        return await base.SendAsync(request, cancellationToken);
    }

    private static void TransformQueryParameters(HttpRequestMessage request)
    {
        if (request.RequestUri == null)
        {
            return;
        }

        var query = HttpUtility.ParseQueryString(request.RequestUri.Query);
        var modified = false;

        foreach (var (parameterName, transformFunc) in Transformations)
        {
            // Get all values for this parameter (handles multiple occurrences)
            var values = query.GetValues(parameterName);
            if (values is { Length: > 0 })
            {
                // Remove all existing values for this parameter
                query.Remove(parameterName);

                // Transform and re-add each value
                foreach (var originalValue in values)
                {
                    var newValue = transformFunc(originalValue);
                    query.Add(parameterName, newValue);

                    if (originalValue != newValue)
                    {
                        modified = true;
                    }
                }
            }
        }

        if (modified)
        {
            var uriBuilder = new UriBuilder(request.RequestUri)
            {
                Query = query.ToString()
            };
            request.RequestUri = uriBuilder.Uri;
        }
    }
}
using System.Text.RegularExpressions;

namespace ExactOnline.Api.Client.Middleware;

/// <summary>
/// Put and Delete requests to Exact Online API require GUIDs to be wrapped in (guid'...') format for Put and Delete requests.
/// Example: …/api/v1/{division}/webhooks/WebhookSubscriptions(guid'{AE0253AA-67AB-480B-9321-F27C50AF22B7}')
/// </summary>
public class GuidWrappingHandler : DelegatingHandler
{
    private static readonly Regex GuidSegmentPattern = new(@"\((?<id>[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12})\)", RegexOptions.Compiled);

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Method == HttpMethod.Put || request.Method == HttpMethod.Delete)
        {
            var uri = request.RequestUri?.ToString();
            if (!string.IsNullOrEmpty(uri))
            {
                var updatedUri = GuidSegmentPattern.Replace(uri, match =>
                {
                    var id = match.Groups["id"].Value;
                    return $"(guid'{id}')";
                });

                if (updatedUri != uri)
                {
                    request.RequestUri = new Uri(updatedUri, UriKind.RelativeOrAbsolute);
                }
            }
        }

        return base.SendAsync(request, cancellationToken);
    }
}
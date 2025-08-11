using ExactOnline.Api.Client.Middleware;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Kiota.Http.HttpClientLibrary.Middleware;

// ReSharper disable once CheckNamespace
namespace ExactOnline.Api.Client;

public partial class ExactOnlineServiceClient
{
    private const string DefaultBaseUrl = "https://start.exactonline.nl";

    public ExactOnlineServiceClient(IAuthenticationProvider authenticationProvider, string baseUrl = DefaultBaseUrl) : this(CreateHttpClientRequestAdapter(authenticationProvider))
    {
        RequestAdapter.BaseUrl = baseUrl;
    }

    private static HttpClientRequestAdapter CreateHttpClientRequestAdapter(IAuthenticationProvider authenticationProvider)
    {
        var handlers = KiotaClientFactory.CreateDefaultHandlers();
        var position = handlers.Select((handler, index) => new { handler, index }).FirstOrDefault(x => x.handler is RetryHandler)?.index ?? -1;
        if (position != -1)
        {
            handlers.Insert(position, new ExactOnlineRateLimitHandler());
        }

        return new HttpClientRequestAdapter(authenticationProvider, httpClient: KiotaClientFactory.Create(handlers))
        {
            BaseUrl = DefaultBaseUrl
        };
    }
}
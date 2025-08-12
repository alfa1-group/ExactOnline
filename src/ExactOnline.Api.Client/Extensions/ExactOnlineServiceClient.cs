using ExactOnline.Api.Client.Middleware;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Kiota.Http.HttpClientLibrary.Middleware;
using Microsoft.Kiota.Http.HttpClientLibrary.Middleware.Options;

// ReSharper disable once CheckNamespace
namespace ExactOnline.Api.Client;

public partial class ExactOnlineServiceClient
{
    private const string DefaultBaseUrl = "https://start.exactonline.nl";

    public ExactOnlineServiceClient(IAuthenticationProvider authenticationProvider, string baseUrl = DefaultBaseUrl, ExactOnlineRateLimitHandler? exactOnlineRateLimitHandler = null)
        : this(CreateHttpClientRequestAdapter(authenticationProvider, exactOnlineRateLimitHandler ?? new ExactOnlineRateLimitHandler()))
    {
        RequestAdapter.BaseUrl = baseUrl;
    }

    private static HttpClientRequestAdapter CreateHttpClientRequestAdapter(IAuthenticationProvider authenticationProvider, ExactOnlineRateLimitHandler exactOnlineRateLimitHandler)
    {
        var handlers = KiotaClientFactory.CreateDefaultHandlers();

        var urlReplacementHandlerPosition = handlers.Select((handler, index) => new { handler, index }).FirstOrDefault(dg => dg.handler is UriReplacementHandler<UriReplacementHandlerOption>)?.index ?? -1;
        if (urlReplacementHandlerPosition != -1)
        {
            handlers.Insert(urlReplacementHandlerPosition + 1, new GuidWrappingHandler());
        }

        var retryHandlerPosition = handlers.Select((handler, index) => new { handler, index }).FirstOrDefault(dg => dg.handler is RetryHandler)?.index ?? -1;
        if (retryHandlerPosition != -1)
        {
            handlers.Insert(retryHandlerPosition, exactOnlineRateLimitHandler);
        }

        return new HttpClientRequestAdapter(authenticationProvider, httpClient: KiotaClientFactory.Create(handlers))
        {
            BaseUrl = DefaultBaseUrl
        };
    }
}
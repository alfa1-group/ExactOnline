using System.Text.Json;
using ExactOnline.Api.Client.JsonConverters;
using ExactOnline.Api.Client.Middleware;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Kiota.Http.HttpClientLibrary.Middleware;
using Microsoft.Kiota.Http.HttpClientLibrary.Middleware.Options;
using Microsoft.Kiota.Serialization.Json;

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
        var handlers = BuildDelegatingHandlers(exactOnlineRateLimitHandler);
        var jsonParseNodeFactory = BuildJsonParseNodeFactory();

        return new HttpClientRequestAdapter(authenticationProvider, jsonParseNodeFactory, httpClient: KiotaClientFactory.Create(handlers))
        {
            BaseUrl = DefaultBaseUrl
        };
    }

    private static IList<DelegatingHandler> BuildDelegatingHandlers(ExactOnlineRateLimitHandler exactOnlineRateLimitHandler)
    {
        var handlers = KiotaClientFactory.CreateDefaultHandlers();

        // 1. Insert the GuidWrappingHandler after the UriReplacementHandler.
        var urlReplacementHandlerPosition = handlers.Select((handler, index) => new { handler, index }).FirstOrDefault(dg => dg.handler is UriReplacementHandler<UriReplacementHandlerOption>)?.index ?? -1;
        if (urlReplacementHandlerPosition != -1)
        {
            handlers.Insert(urlReplacementHandlerPosition + 2, new GuidWrappingHandler());
        }

        // 2. Replace the RetryHandler by ExactOnlineRateLimitHandler.
        var retryHandlerPosition = handlers.Select((handler, index) => new { handler, index }).FirstOrDefault(dg => dg.handler is RetryHandler)?.index ?? -1;
        if (retryHandlerPosition != -1)
        {
            handlers.RemoveAt(retryHandlerPosition);
            handlers.Insert(retryHandlerPosition, exactOnlineRateLimitHandler);
        }

        return handlers;
    }

    private static JsonParseNodeFactory BuildJsonParseNodeFactory()
    {
        // 1. Create JsonSerializerOptions and add the custom Legacy DateTime converters.
        var customOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new LegacyDateTimeOffsetConverter(),
                new LegacyDateTimeConverter()
            }
        };

        // 2. Create the Kiota JSON Parse Node Factory with the custom options.
        var kiotaJsonSerializationContext = new KiotaJsonSerializationContext(customOptions);

        // 3. Create the JsonParseNodeFactory with the KiotaJsonSerializationContext.
        return new JsonParseNodeFactory(kiotaJsonSerializationContext);
    }
}
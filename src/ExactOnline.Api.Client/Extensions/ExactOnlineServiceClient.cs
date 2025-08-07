using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

// ReSharper disable once CheckNamespace
namespace ExactOnline.Api.Client;

public partial class ExactOnlineServiceClient
{
    private const string DefaultBaseUrl = "https://start.exactonline.nl";

    public ExactOnlineServiceClient(IAuthenticationProvider authenticationProvider, string baseUrl = DefaultBaseUrl) : this(new HttpClientRequestAdapter(authenticationProvider))
    {
        RequestAdapter.BaseUrl = baseUrl;
    }
}
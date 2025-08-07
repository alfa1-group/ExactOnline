using Duende.IdentityModel.Client;
using ExactOnline.Api.Client.Authentication.Interfaces;
using ExactOnline.Api.Client.Authentication.Options;
using Microsoft.Extensions.Options;

namespace ExactOnline.Api.Client.Authentication.Implementations;

internal class ExactTokenClient : IExactTokenClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly TokenClientOptions _tokenClientOptions;

    public ExactTokenClient(IHttpClientFactory httpClientFactory, IOptions<ExactIntegrationOptions> exactOptions)
    {
        _httpClientFactory = httpClientFactory;

        _tokenClientOptions = new TokenClientOptions
        {
            Address = exactOptions.Value.Instance + "/api/oauth2/token",
            ClientId = exactOptions.Value.ClientId,
            ClientSecret = exactOptions.Value.ClientSecret
        };

    }

    public async Task<TokenResponse> RequestRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var tokenHttpClient = _httpClientFactory.CreateClient();
        var client = new TokenClient(tokenHttpClient, _tokenClientOptions);
        return await client.RequestRefreshTokenAsync(refreshToken, cancellationToken: cancellationToken);
    }
}

using Duende.IdentityModel.Client;
using ExactOnline.Api.Client.Authentication.Interfaces;
using ExactOnline.Api.Client.Authentication.Options;
using Microsoft.Extensions.Options;

namespace ExactOnline.Api.Client.Authentication.Implementations;

internal class ExactTokenClient(IHttpClientFactory httpClientFactory, IOptions<ExactOnlineOptions> exactOptions) : IExactTokenClient
{
    private readonly TokenClientOptions _tokenClientOptions = new()
    {
        Address = exactOptions.Value.BaseUrl + "/api/oauth2/token",
        ClientId = exactOptions.Value.ClientId,
        ClientSecret = exactOptions.Value.ClientSecret
    };

    public async Task<TokenResponse> RequestRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var tokenHttpClient = httpClientFactory.CreateClient();
        var client = new TokenClient(tokenHttpClient, _tokenClientOptions);

        return await client.RequestRefreshTokenAsync(refreshToken, cancellationToken: cancellationToken);
    }
}
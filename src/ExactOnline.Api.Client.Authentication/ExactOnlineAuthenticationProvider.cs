using ExactOnline.Api.Client.Authentication.Interfaces;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace ExactOnline.Api.Client.Authentication;

internal class ExactOnlineAuthenticationProvider(IExactTokenService tokenService) : IAuthenticationProvider
{
    private const string AuthorizationHeaderKey = "Authorization";

    public async Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        if (!request.Headers.ContainsKey(AuthorizationHeaderKey))
        {
            var token = await tokenService.GetAccessTokenAsync();

            request.Headers.Add(AuthorizationHeaderKey, $"Bearer {token}");
        }
    }
}
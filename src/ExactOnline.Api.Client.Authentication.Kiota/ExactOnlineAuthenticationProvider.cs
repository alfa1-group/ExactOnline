using ExactOnline.Api.Client.Authentication.Interfaces;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace ExactOnline.Api.Client.Authentication.Kiota;

internal class ExactOnlineAuthenticationProvider(IExactTokenService tokenService) : IAuthenticationProvider
{
    private const string AuthorizationHeaderKey = "Authorization";

    public async Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        if (!request.Headers.ContainsKey(AuthorizationHeaderKey))
        {
            var accessToken = await tokenService.GetAccessTokenAsync(cancellationToken);

            request.Headers.Add(AuthorizationHeaderKey, $"Bearer {accessToken}");
        }
    }
}
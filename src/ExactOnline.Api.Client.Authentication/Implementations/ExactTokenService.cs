using Duende.IdentityModel.Client;
using ExactOnline.Api.Client.Authentication.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ExactOnline.Api.Client.Authentication.Implementations;

internal class ExactTokenService(
    ILogger<ExactTokenService> logger,
    IExactRefreshTokenStorageService tokenStorageService,
    IMemoryCache memoryCache,
    IExactTokenClient exactTokenClient) : IExactTokenService
{
    private const string ExactAccessTokenKey = "ExactAccessToken";
    private const int RateLimitDelayInMinutes = 1;

    // The expiration time to 9 minutes and 30 seconds, which is the maximum time a token is valid.
    private readonly TimeSpan _accessTokenExpirationTime = TimeSpan.FromSeconds(9 * 60 + 30);

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        // First we check if we have a token in memory.
        if (memoryCache.TryGetValue(ExactAccessTokenKey, out string? accessToken))
        {
            // The memory cache entry is valid for 9 minutes and 30 seconds, which means that after this time we won't get a cached token returned and we should refresh the token using the refresh token.
            // Checking the validity of the token itself is not possible, because the token is encrypted and we don't have the private key to decrypt it.
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                return accessToken;
            }
        }

        // If expired, refresh the AccessToken and fetch the token from storage.
        return await RefreshTokenAsync(cancellationToken);
    }

    public async Task<string> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        // For refreshing the token we first need to fetch the current refresh token from storage
        var refreshToken = await tokenStorageService.RetrieveAsync(cancellationToken);

        // The client will issue the refresh request and should get a fresh refresh token + access token in response
        var response = await RequestRefreshTokenWithRetryAndErrorHandlingAsync(refreshToken, cancellationToken);

        // Store the new refresh token back in storage as the previous one is now invalid.
        try
        {
            await tokenStorageService.StoreAsync(response.RefreshToken, cancellationToken);
        }
        catch (Exception ex)
        {
            // This is a bit nasty because it leaves the RefreshToken in the logs.
            // But that way we can at least track it down where otherwise we need to generate a complete new token from Exact again, which requires admin.
            throw new Exception($"Uploading the new RefreshToken failed. Here is the token: {response.RefreshToken}", ex);
        }

        if (string.IsNullOrWhiteSpace(response.AccessToken))
        {
            logger.LogError("The access token is null or empty. ({ErrorType} {Error} {ErrorDescription}).", response.ErrorType, response.Error, response.ErrorDescription);
        }

        // Store the access token in memory for reuse
        return memoryCache.Set(ExactAccessTokenKey, response.AccessToken, _accessTokenExpirationTime)!;
    }

    private async Task<TokenResponse> RequestRefreshTokenWithRetryAndErrorHandlingAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var startTime = TimeProvider.System.GetUtcNow();

        while (true)
        {
            var response = await exactTokenClient.RequestRefreshTokenAsync(refreshToken, cancellationToken);

            if (string.IsNullOrWhiteSpace(response.RefreshToken) && (IsRateLimitExceeded(response) || IsHttpFault(response)))
            {
                var elapsedTime = TimeProvider.System.GetUtcNow() - startTime;
                if (elapsedTime > _accessTokenExpirationTime)
                {
                    throw new Exception($"AccessToken cannot be retrieved due to rate limiting and timeout exceeded ({_accessTokenExpirationTime}).");
                }

                logger.LogInformation("Rate limit exceeded for access token. Retrying in {Delay} seconds.", RateLimitDelayInMinutes);
                await Task.Delay(TimeSpan.FromSeconds(RateLimitDelayInMinutes), cancellationToken);

                continue;
            }

            if (string.IsNullOrWhiteSpace(response.RefreshToken) && response.ErrorDescription?.IndexOf("expired", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                logger.LogError("The Exact refresh token has expired ({ErrorType} {Error} {ErrorDescription}). You need to update the refresh token stored in the storage with a fresh token from Exact.",
                    response.ErrorType, response.Error, response.ErrorDescription);
                throw new Exception("The Exact refresh token has expired.");
            }

            if (string.IsNullOrWhiteSpace(response.RefreshToken))
            {
                logger.LogError("There was a problem fetching a new auth token from Exact. ({ErrorType} {Error} {ErrorDescription}).",
                    response.ErrorType, response.Error, response.ErrorDescription);
                throw new Exception("Exact did not return a new auth token.", response.Exception);
            }

            return response;
        }
    }

    private static bool IsRateLimitExceeded(TokenResponse response)
    {
        return response.ErrorDescription?.IndexOf("Rate limit exceeded: access_token not expired", StringComparison.InvariantCultureIgnoreCase) >= 0;
    }

    private static bool IsHttpFault(TokenResponse response)
    {
        if (response.ErrorType == ResponseErrorType.Http)
        {
            var fault = response.TryGet("fault");
            // { "faultstring":"Unable to identify proxy for host: start.exactonline.nl:443 and url: \/api\/oauth2\/token","detail":{ "errorcode":"messaging.adaptors.http.flow.ApplicationNotFound"} }
            return !string.IsNullOrWhiteSpace(fault) && fault.Contains(@"Unable to identify proxy for host: start.exactonline.nl:443 and url: \/api\/oauth2\/token");
        }

        return false;
    }
}
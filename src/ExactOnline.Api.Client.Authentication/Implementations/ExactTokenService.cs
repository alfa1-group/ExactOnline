using System.Net;
using System.Text.Json;
using Alfa1.TokenStorage.Abstractions;
using Duende.IdentityModel.Client;
using ExactOnline.Api.Client.Authentication.Interfaces;
using Microsoft.Extensions.Logging;

namespace ExactOnline.Api.Client.Authentication.Implementations;

internal class ExactTokenService(
    ILogger<ExactTokenService> logger,
    ITokenStorageService tokenStorageService,
    IExactTokenClient exactTokenClient,
    TimeProvider timeProvider) : IExactTokenService
{
    private const int RateLimitDelayInMinutes = 1;

    // The expiration time to 9 minutes and 30 seconds, which is the maximum time a token is valid.
    private readonly TimeSpan _accessTokenExpirationTime = TimeSpan.FromSeconds(9 * 60 + 30);

    // Ensure that only one thread refreshes the tokens at a time
    private static readonly SemaphoreSlim RefreshTokenSemaphore = new(1, 1);

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        // First we check if we have a token in storage.
        var accessToken = await tokenStorageService.RetrieveAccessTokenAsync(cancellationToken);
        if (!string.IsNullOrEmpty(accessToken))
        {
            // The cache entry is valid for 9 minutes and 30 seconds, which means that after this time we won't get a cached token returned and we should refresh the token using the refresh token.
            // Checking the validity of the token itself is not possible, because the token is encrypted and we don't have the private key to decrypt it.
            return accessToken;
        }

        // If expired or not present, refresh the AccessToken by contacting the authentication server using the refresh token from storage.
        return await RefreshTokenAsync(cancellationToken);
    }

    public async Task<string> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        await RefreshTokenSemaphore.WaitAsync(cancellationToken);

        try
        {
            // The client will issue the refresh request and should get a fresh refresh token + access token in response
            var (currentRefreshToken, response) = await RequestRefreshTokenWithRetryAndErrorHandlingAsync(cancellationToken);

            // Store the new refresh token back in storage as the previous one is now invalid.
            try
            {
                await tokenStorageService.StoreRefreshTokenAsync(currentRefreshToken, response.RefreshToken!, cancellationToken);
            }
            catch (Exception ex)
            {
                // This is a bit nasty because it leaves the RefreshToken in the logs.
                // But that way we can at least track it down where otherwise we need to generate a complete new token from Exact again, which requires admin.
                throw new Exception($"Saving the new RefreshToken failed. Here is the token: {response.RefreshToken}", ex);
            }

            if (string.IsNullOrWhiteSpace(response.AccessToken))
            {
                logger.LogError("The access token is null or empty. ({ErrorType} {Error} {ErrorDescription}).", response.ErrorType, response.Error, response.ErrorDescription);
            }

            // Store the access token for reuse
            var currentAccessToken = await tokenStorageService.RetrieveAccessTokenAsync(cancellationToken);
            return await tokenStorageService.StoreAccessTokenAsync(currentAccessToken, response.AccessToken!, _accessTokenExpirationTime, cancellationToken);
        }
        finally
        {
            RefreshTokenSemaphore.Release();
        }
    }

    private async Task<(string CurrentRefreshToken, TokenResponse TokenResponse)> RequestRefreshTokenWithRetryAndErrorHandlingAsync(CancellationToken cancellationToken)
    {
        var startTime = timeProvider.GetUtcNow();

        while (true)
        {
            // For refreshing the token we first need to fetch the current refresh token from database
            var currentRefreshToken = await tokenStorageService.RetrieveRefreshTokenAsync(cancellationToken);

            // Now we can request a new access token using the refresh token
            var response = await exactTokenClient.RequestRefreshTokenAsync(currentRefreshToken, cancellationToken);

            if (string.IsNullOrWhiteSpace(response.RefreshToken))
            {
                var elapsedTime = timeProvider.GetUtcNow() - startTime;

                if (IsRateLimitExceeded(response))
                {
                    await DelayOrThrowExceptionAsync(elapsedTime, "Rate limit exceeded", cancellationToken);
                    continue;
                }

                if (IsHttpProxyFault(response))
                {
                    await DelayOrThrowExceptionAsync(elapsedTime, "Http Proxy Fault", cancellationToken);
                    continue;
                }

                if (response.ErrorDescription?.IndexOf("expired", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    logger.LogError("The Exact refresh token has expired ({ErrorType} {Error} {ErrorDescription}). You need to update the refresh token stored in the storage with a fresh token from Exact.",
                        response.ErrorType, response.Error, response.ErrorDescription);
                    throw new Exception("The Exact refresh token has expired.");
                }

                logger.LogError("There was a problem fetching a new auth token from Exact. ({ErrorType} {Error} {ErrorDescription}).",
                    response.ErrorType, response.Error, response.ErrorDescription);
                logger.LogDebug("There was a problem fetching a new auth token from Exact. {TokenResponse}", JsonSerializer.Serialize(response));
                throw new Exception("Exact did not return a new auth token.", response.Exception);
            }

            return (currentRefreshToken, response);
        }
    }

    private Task DelayOrThrowExceptionAsync(TimeSpan elapsedTime, string error, CancellationToken cancellationToken)
    {
        if (elapsedTime > _accessTokenExpirationTime)
        {
            throw new Exception($"AccessToken cannot be retrieved due to '{error}' and timeout exceeded ({_accessTokenExpirationTime}).");
        }

        logger.LogInformation("{Error} for access token. Retrying in {Delay} minutes.", error, RateLimitDelayInMinutes);
        return Task.Delay(TimeSpan.FromMinutes(RateLimitDelayInMinutes), cancellationToken);
    }

    private static bool IsRateLimitExceeded(TokenResponse response)
    {
        return response.HttpResponse?.StatusCode == HttpStatusCode.BadRequest &&
               response.ErrorDescription?.Contains("Rate limit exceeded: access_token not expired", StringComparison.OrdinalIgnoreCase) == true;
    }

    private static bool IsHttpProxyFault(TokenResponse response)
    {
        if (response.ErrorType == ResponseErrorType.Http)
        {
            var fault = response.TryGet("fault");
            // { "faultstring":"Unable to identify proxy for host: start.exactonline.nl:443 and url: \/api\/oauth2\/token","detail":{ "errorcode":"messaging.adaptors.http.flow.ApplicationNotFound"} }
            return !string.IsNullOrWhiteSpace(fault) && fault.Contains(@"Unable to identify proxy for host: start.exactonline.nl:443 and url: \/api\/oauth2\/token", StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }
}
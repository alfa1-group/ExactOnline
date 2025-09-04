using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.SqlServer.Data;
using ExactOnline.Api.Client.Authentication.Storage.SqlServer.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExactOnline.Api.Client.Authentication.Storage.SqlServer;

internal class ExactTokenStorageEntityFrameworkCoreService(
    ILogger<ExactTokenStorageEntityFrameworkCoreService> logger,
    IOptions<ExactOnlineEntityFrameworkCoreStorageOptions> options,
    IMemoryCache memoryCache,
    ExactOnlineTokenDbContext dbContext,
    TimeProvider timeProvider) : IExactTokenStorageService
{
    public async Task<string> StoreRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var existingToken = await dbContext.Tokens.SingleOrDefaultAsync(cancellationToken);
        if (existingToken != null)
        {
            existingToken.RefreshToken = refreshToken;
            existingToken.RefreshTokenUpdatedAt = timeProvider.GetUtcNow();
        }
        else
        {
            dbContext.Tokens.Add(new() { RefreshToken = refreshToken, RefreshTokenUpdatedAt = timeProvider.GetUtcNow() });
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return refreshToken;
    }

    public async Task<string> RetrieveRefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        var refreshToken = await dbContext.Tokens.SingleOrDefaultAsync(cancellationToken);
        if (refreshToken == null)
        {
            logger.LogInformation("Token entity does not exist in table {Table}. Returning empty string.", options.Value.TableName);
            return string.Empty;
        }

        return refreshToken.RefreshToken;
    }

    public async Task<string> RetrieveAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        // 1. Try to retrieve the access token from memory cache for quick access.
        if (memoryCache.TryGetValue(options.Value.AccessTokenColumnName, out string? accessToken) && !string.IsNullOrEmpty(accessToken))
        {
            return accessToken;
        }

        // 2. If not found in memory cache, retrieve it from database.
        var token = await dbContext.Tokens.SingleOrDefaultAsync(cancellationToken);
        if (token == null)
        {
            logger.LogInformation("Token entity does not exist in table {Table}. Returning empty string.", options.Value.TableName);
            return string.Empty;
        }

        if (string.IsNullOrEmpty(token.AccessToken))
        {
            logger.LogInformation("AccessToken is null or empty in table {Table}. Returning empty string.", options.Value.TableName);
            return string.Empty;
        }

        if (timeProvider.GetUtcNow() <= token.AccessTokenExpire)
        {
            return memoryCache.Set(options.Value.AccessTokenColumnName, token.AccessToken, token.AccessTokenExpire);
        }

        logger.LogInformation("AccessToken blob is expired. Returning empty string value.");
        return string.Empty;
    }

    public async Task<string> StoreAccessTokenAsync(string accessToken, TimeSpan absoluteExpirationRelativeToUtcNow, CancellationToken cancellationToken = default)
    {
        // 1. Store the access token in memory cache for quick access.
        memoryCache.Set(options.Value.AccessTokenColumnName, accessToken, absoluteExpirationRelativeToUtcNow);

        // 2. Store the access token in database
        var token = await dbContext.Tokens.SingleOrDefaultAsync(cancellationToken);
        if (token == null)
        {
            logger.LogInformation("Token entity does not exist in table {Table}. Returning empty string.", options.Value.TableName);
            return string.Empty;
        }

        var now = timeProvider.GetUtcNow();
        token.AccessToken = accessToken;
        token.AccessTokenUpdatedAt = now;
        token.AccessTokenExpire = now.Add(absoluteExpirationRelativeToUtcNow);

        await dbContext.SaveChangesAsync(cancellationToken);

        return accessToken;
    }
}
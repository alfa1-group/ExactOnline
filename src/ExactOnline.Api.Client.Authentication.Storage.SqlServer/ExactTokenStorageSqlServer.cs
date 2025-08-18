using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.SqlServer.Data;
using ExactOnline.Api.Client.Authentication.Storage.SqlServer.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExactOnline.Api.Client.Authentication.Storage.SqlServer;

internal class ExactTokenStorageSqlServer(
    ILogger<ExactTokenStorageSqlServer> logger,
    IOptions<ExactOnlineSqlServerStorageOptions> options,
    IMemoryCache memoryCache,
    ExactOnlineTokenDbContext dbContext) : IExactTokenStorageService
{
    private const string AccessTokenKey = nameof(AccessTokenKey);

    public async Task StoreRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var existingToken = await dbContext.RefreshTokens.SingleOrDefaultAsync(cancellationToken);
        if (existingToken != null)
        {
            existingToken.RefreshToken = refreshToken;
            existingToken.RefreshTokenUpdatedAt = TimeProvider.System.GetUtcNow();
        }
        else
        {
            dbContext.RefreshTokens.Add(new() { RefreshToken = refreshToken, RefreshTokenUpdatedAt = TimeProvider.System.GetUtcNow() });
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<string> RetrieveRefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        var refreshToken = await dbContext.RefreshTokens.SingleOrDefaultAsync(cancellationToken);
        if (refreshToken == null)
        {
            logger.LogInformation("RefreshToken entity does not exist in table {Table}. Returning empty string.", options.Value.TableName);
            return string.Empty;
        }

        return refreshToken.RefreshToken;
    }

    public Task<string> RetrieveAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        if (memoryCache.TryGetValue(AccessTokenKey, out string? accessToken) && !string.IsNullOrEmpty(accessToken))
        {
            return Task.FromResult(accessToken);
        }

        return Task.FromResult(string.Empty);
    }

    public Task<string> StoreAccessTokenAsync(string accessToken, TimeSpan absoluteExpirationRelativeToUtcNow, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(memoryCache.Set(AccessTokenKey, accessToken, absoluteExpirationRelativeToUtcNow));
    }
}
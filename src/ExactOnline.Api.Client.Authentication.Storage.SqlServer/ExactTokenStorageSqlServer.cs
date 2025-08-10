using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.SqlServer.Data;
using ExactOnline.Api.Client.Authentication.Storage.SqlServer.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExactOnline.Api.Client.Authentication.Storage.SqlServer;

internal class ExactTokenStorageSqlServer(
    ILogger<ExactTokenStorageSqlServer> logger,
    IOptions<ExactOnlineSqlServerStorageOptions> options,
    ExactOnlineTokenDbContext dbContext) : IExactTokenStorageService
{
    public async Task StoreRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        dbContext.RefreshTokens.RemoveRange(dbContext.RefreshTokens);
        dbContext.RefreshTokens.Add(new() { RefreshToken = refreshToken, RefreshTokenUpdatedAt = TimeProvider.System.GetUtcNow() });

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<string> RetrieveRefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);

        var refreshToken = await dbContext.RefreshTokens.SingleOrDefaultAsync(cancellationToken);
        if (refreshToken == null)
        {
            logger.LogInformation("RefreshToken entity does not exist in table {Table}. Returning empty string.", options.Value.TableName);
            return string.Empty;
        }

        if (string.IsNullOrEmpty(refreshToken.RefreshToken))
        {
            logger.LogInformation("RefreshToken in table {Table} with column {Column} is null or empty. Returning empty string.", options.Value.TableName, options.Value.ColumnName);
            return string.Empty;
        }

        return refreshToken.RefreshToken;
    }
}
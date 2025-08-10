using System;
using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.SqlServer.Data;
using Microsoft.EntityFrameworkCore;

namespace ExactOnline.Api.Client.Authentication.Storage.SqlServer;

internal class ExactTokenStorageSqlServer(IDbContextFactory<ExactOnlineTokenDbContext> dbContextFactory) : IExactTokenStorageService
{
	public async Task StoreRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
	{
		await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
		await dbContext.Database.EnsureCreatedAsync(cancellationToken);
		dbContext.RefreshTokens.RemoveRange(dbContext.RefreshTokens);
		dbContext.RefreshTokens.Add(new ExactOnlineToken { RefreshToken = refreshToken, RefreshTokenUpdatedAt = TimeProvider.System.GetUtcNow() });
		await dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<string> RetrieveRefreshTokenAsync(CancellationToken cancellationToken = default)
	{
		await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
		await dbContext.Database.EnsureCreatedAsync(cancellationToken);
		var refreshToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(cancellationToken);
		if (refreshToken == null)
		{
			throw new InvalidOperationException("No refresh token found in the database.");
		}
		return refreshToken.RefreshToken;
	}
}

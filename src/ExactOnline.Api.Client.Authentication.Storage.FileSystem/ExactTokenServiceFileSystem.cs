using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.FileSystem.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExactOnline.Api.Client.Authentication.Storage.FileSystem;

internal class ExactTokenServiceFileSystem(ILogger<ExactTokenServiceFileSystem> logger, IOptions<ExactOnlineFileSystemOptions> options, IMemoryCache memoryCache) : IExactTokenStorageService
{
    private readonly string _refreshTokenFilePath = options.Value.RefreshTokenFilePath;

    public async Task<string> RetrieveRefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(_refreshTokenFilePath))
        {
            logger.LogInformation("RefreshToken file does not exist at path: {FilePath}. Returning empty string value.", _refreshTokenFilePath);
            return string.Empty;
        }

        return await File.ReadAllTextAsync(_refreshTokenFilePath, cancellationToken);
    }

    public Task StoreRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        return File.WriteAllTextAsync(_refreshTokenFilePath, refreshToken, cancellationToken);
    }

    public Task<string> RetrieveAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        if (memoryCache.TryGetValue(options.Value.AccessTokenFilePath, out string? accessToken) && !string.IsNullOrEmpty(accessToken))
        {
            return Task.FromResult(accessToken);
        }

        return Task.FromResult(string.Empty);
    }

    public Task<string> StoreAccessTokenAsync(string accessToken, TimeSpan absoluteExpirationRelativeToUtcNow, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(memoryCache.Set(options.Value.AccessTokenFilePath, accessToken, absoluteExpirationRelativeToUtcNow));
    }
}
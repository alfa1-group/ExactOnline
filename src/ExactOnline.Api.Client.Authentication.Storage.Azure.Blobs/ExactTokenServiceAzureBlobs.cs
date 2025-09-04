using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs;

internal class ExactTokenServiceAzureBlobs(
    ILogger<ExactTokenServiceAzureBlobs> logger,
    IOptions<ExactOnlineAzureBlobsStorageOptions> options,
    IMemoryCache memoryCache,
    BlobContainerClient blobContainerClient, 
    TimeProvider timeProvider) : IExactTokenStorageService
{
    private const string MetadataAbsoluteExpirationRelativeToUtcNow = "AbsoluteExpirationRelativeToUtcNow";

    private readonly BlobClient _refreshTokenBlobClient = blobContainerClient.GetBlobClient(options.Value.RefreshTokenFilePath);
    private readonly BlobClient _accessTokenBlobClient = blobContainerClient.GetBlobClient(options.Value.AccessTokenFilePath);

    public async Task<string> RetrieveRefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        if (!await _refreshTokenBlobClient.ExistsAsync(cancellationToken))
        {
            logger.LogInformation("RefreshToken blob does not exist in container {Container} at path: {FilePath}. Returning empty string value.", options.Value.ContainerName, options.Value.RefreshTokenFilePath);
            return string.Empty;
        }

        var response = await _refreshTokenBlobClient.DownloadContentAsync(cancellationToken);
        return response.Value.Content.ToString();
    }

    public async Task<string> StoreRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        _ = await _refreshTokenBlobClient.UploadAsync(BinaryData.FromString(refreshToken), overwrite: true, cancellationToken);

        return refreshToken;
    }

    public async Task<string> RetrieveAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        // 1. Try to retrieve the access token from memory cache for quick access.
        if (memoryCache.TryGetValue(options.Value.AccessTokenFilePath, out string? accessToken) && !string.IsNullOrEmpty(accessToken))
        {
            return accessToken;
        }

        // 2. If not found in memory cache, retrieve it from Azure Blob Storage.
        if (!await _accessTokenBlobClient.ExistsAsync(cancellationToken))
        {
            logger.LogInformation("AccessToken blob does not exist in container {Container} at path: {FilePath}. Returning empty string value.", options.Value.ContainerName, options.Value.AccessTokenFilePath);
            return string.Empty;
        }

        var response = await _accessTokenBlobClient.DownloadContentAsync(cancellationToken);
        var metadata = response.Value.Details.Metadata;
        if (metadata.TryGetValue(MetadataAbsoluteExpirationRelativeToUtcNow, out var metadataValue)
            && DateTimeOffset.TryParse(metadataValue, out var absoluteExpirationRelativeToNow)
            && timeProvider.GetUtcNow() <= absoluteExpirationRelativeToNow
        )
        {
            return memoryCache.Set(options.Value.AccessTokenFilePath, response.Value.Content.ToString(), absoluteExpirationRelativeToNow);
        }

        logger.LogInformation("AccessToken blob is expired or does not have a valid Metadata. Returning empty string value.");
        return string.Empty;
    }

    public async Task<string> StoreAccessTokenAsync(string accessToken, TimeSpan absoluteExpirationRelativeToNow, CancellationToken cancellationToken = default)
    {
        // 1. Store the access token in memory cache for quick access.
        memoryCache.Set(options.Value.AccessTokenFilePath, accessToken, absoluteExpirationRelativeToNow);

        // 2. Store the access token in Azure Blob Storage with metadata for absolute expiration.
        var blobUploadOptions = new BlobUploadOptions
        {
            Conditions = new BlobRequestConditions
            {
                // No conditions = always overwrite
            },
            Metadata = new Dictionary<string, string>
            {
                [MetadataAbsoluteExpirationRelativeToUtcNow] = timeProvider.GetUtcNow().Add(absoluteExpirationRelativeToNow).ToString("O")
            }
        };

        _ = await _accessTokenBlobClient.UploadAsync(BinaryData.FromString(accessToken), blobUploadOptions, cancellationToken);
        return accessToken;
    }
}
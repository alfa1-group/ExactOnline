using Azure.Storage.Blobs;
using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs;

internal class ExactTokenServiceAzureBlobs(
    ILogger<ExactTokenServiceAzureBlobs> logger,
    IOptions<ExactOnlineAzureBlobsStorageOptions> options,
    BlobClient blobClient) : IExactTokenStorageService
{
    public async Task<string> RetrieveRefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        if (!await blobClient.ExistsAsync(cancellationToken))
        {
            logger.LogInformation("RefreshToken blob does not exist in container {Container} at path: {FilePath}. Returning empty string value.", options.Value.ContainerName, options.Value.RefreshTokenFilePath);
            return string.Empty;
        }

        var response = await blobClient.DownloadContentAsync(cancellationToken);
        return response.Value.Content.ToString();
    }

    public Task StoreRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        return blobClient.UploadAsync(BinaryData.FromString(refreshToken), overwrite: true, cancellationToken);
    }
}
using Azure.Storage.Blobs;
using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs.Options;
using Microsoft.Extensions.Options;

namespace ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs;

public class ExactTokenBlobStorageService(IOptions<ExactOnlineAzureBlobStorageOptions> options) : IExactTokenStorageService
{
    public async Task<string> RetrieveRefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        var container = await GetBlobContainerAsync(cancellationToken);
        var blobClient = container.GetBlobClient(options.Value.FilePath);

        if (!await blobClient.ExistsAsync(cancellationToken))
        {
            return string.Empty;
        }

        var response = await blobClient.DownloadContentAsync(cancellationToken);
        return response.Value.Content.ToString();
    }

    public async Task StoreRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var container = await GetBlobContainerAsync(cancellationToken);
        var blobClient = container.GetBlobClient(options.Value.FilePath);

        await blobClient.UploadAsync(BinaryData.FromString(refreshToken), overwrite: true, cancellationToken);
    }

    private async Task<BlobContainerClient> GetBlobContainerAsync(CancellationToken cancellationToken)
    {
        var container = new BlobContainerClient(options.Value.ConnectionString, options.Value.BlobContainerName);

        await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        return container;
    }
}
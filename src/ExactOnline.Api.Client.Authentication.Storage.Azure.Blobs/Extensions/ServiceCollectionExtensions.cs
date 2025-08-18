using Azure.Storage.Blobs;
using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs;
using ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExactOnlineTokenStorageAzureBlobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ExactOnlineAzureBlobsStorageOptions>()
            .Bind(configuration.GetSection(nameof(ExactOnlineAzureBlobsStorageOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddMemoryCache();

        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<ExactOnlineAzureBlobsStorageOptions>>();
            return new BlobContainerClient(options.Value.ConnectionString, options.Value.ContainerName);
        });

        return services.AddSingleton<IExactTokenStorageService, ExactTokenServiceAzureBlobs>();
    }
}
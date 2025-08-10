using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs;
using ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs.Options;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExactOnlineTokenStorageAzureBlobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ExactOnlineAzureBlobStorageOptions>()
            .Bind(configuration.GetSection(nameof(ExactOnlineAzureBlobStorageOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        return services.AddSingleton<IExactTokenStorageService, ExactTokenBlobStorageService>();
    }
}
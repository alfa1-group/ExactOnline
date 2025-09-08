using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.FileSystem;
using ExactOnline.Api.Client.Authentication.Storage.FileSystem.Options;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExactOnlineTokenStorageFileSystem(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ExactOnlineFileSystemOptions>()
            .Bind(configuration.GetSection(nameof(ExactOnlineFileSystemOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddMemoryCache();

        return services.AddSingleton<IExactTokenStorageService, ExactTokenServiceFileSystem>();
    }
}
using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.SqlServer;
using ExactOnline.Api.Client.Authentication.Storage.SqlServer.Options;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExactOnlineTokenStorageAzureBlobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ExactOnlineSqlServerStorageOptions>()
            .Bind(configuration.GetSection(nameof(ExactOnlineSqlServerStorageOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services.AddSingleton<IExactTokenStorageService, ExactTokenStorageSqlServer>();
    }
}
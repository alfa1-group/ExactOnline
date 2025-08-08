using ExactOnline.Api.Client.Authentication.Implementations;
using ExactOnline.Api.Client.Authentication.Interfaces;
using ExactOnline.Api.Client.Authentication.Options;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for setting up Exact Online services in an <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExactOnlineAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ExactOnlineOptions>()
            .Bind(configuration.GetSection(nameof(ExactOnlineOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddHttpClient();
        services.AddMemoryCache();
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IExactTokenClient, ExactTokenClient>();
        services.AddSingleton<IExactTokenService, ExactTokenService>();
        
        try
        {
            _ = services.BuildServiceProvider().GetRequiredService<IExactRefreshTokenStorageService>();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An implementation for {nameof(IExactRefreshTokenStorageService)} is required. Please register it in the service collection.", ex);
        }

        return services;
    }
}
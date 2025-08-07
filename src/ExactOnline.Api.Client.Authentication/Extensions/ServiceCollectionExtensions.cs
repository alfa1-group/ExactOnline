using ExactOnline.Api.Client;
using ExactOnline.Api.Client.Authentication;
using ExactOnline.Api.Client.Authentication.Implementations;
using ExactOnline.Api.Client.Authentication.Interfaces;
using ExactOnline.Api.Client.Authentication.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for setting up Exact Online services in an <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExactOnlineAuthenticatedClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptionsWithValidateOnStart<ExactOnlineOptions>();
        services.AddHttpClient();
        services.AddMemoryCache();
        services.AddServices();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<ExactOnlineAuthenticationProvider>();
        services.AddSingleton<IExactTokenClient, ExactTokenClient>();
        services.AddSingleton<IExactTokenService, ExactTokenService>();

        return services.AddScoped(sp =>
        {
            try
            {
                _ = sp.GetRequiredService<IExactRefreshTokenStorageService>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"The {nameof(IExactRefreshTokenStorageService)} is required for {nameof(ExactOnlineServiceClient)}. Please register it in the service collection.", ex);
            }

            var authenticationProvider = sp.GetRequiredService<ExactOnlineAuthenticationProvider>();
            var options = sp.GetRequiredService<IOptions<ExactOnlineOptions>>();

            return new ExactOnlineServiceClient(authenticationProvider, options.Value.BaseUrl);
        });
    }
}
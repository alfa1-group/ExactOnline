using ExactOnline.Api.Client;
using ExactOnline.Api.Client.Authentication.Kiota;
using ExactOnline.Api.Client.Authentication.Options;
using ExactOnline.Api.Client.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Setup Dependency Injection for the <see cref="ExactOnlineServiceClient"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExactOnlineKiotaAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExactOnlineAuthentication(configuration);
        services.AddSingleton<ExactOnlineAuthenticationProvider>();
        services.AddSingleton<ExactOnlineRateLimitHandler>();
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped(sp =>
        {
            var authenticationProvider = sp.GetRequiredService<ExactOnlineAuthenticationProvider>();
            var options = sp.GetRequiredService<IOptions<ExactOnlineOptions>>();
            var exactOnlineRateLimitHandler = sp.GetRequiredService<ExactOnlineRateLimitHandler>();

            return new ExactOnlineServiceClient(authenticationProvider, exactOnlineRateLimitHandler, options.Value.BaseUrl);
        });
    }
}
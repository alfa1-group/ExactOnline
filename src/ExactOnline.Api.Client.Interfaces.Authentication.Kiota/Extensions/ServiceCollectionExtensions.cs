using ExactOnline.Api.Client;
using ExactOnline.Api.Client.Authentication.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Kiota.Abstractions.Authentication;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Setup Dependency Injection for the <see cref="IExactOnlineServiceClient"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExactOnlineInterfacesKiotaAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExactOnlineKiotaAuthentication(configuration);
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<IExactOnlineServiceClient>(sp =>
        {
            var client = sp.GetRequiredService<ExactOnlineServiceClient>();

            return new ExactOnlineServiceClientProxy(client);
        });
    }
}
//using ExactOnline.Api.Client.Authentication.Implementations;
//using ExactOnline.Api.Client.Authentication.Interfaces;
//using ExactOnline.Api.Client.Authentication.Options;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace ExactOnline.Api.Client.Authentication.Extensions;

///// <summary>
///// Extension methods for setting up localization services in an <see cref="IServiceCollection" />.
///// </summary>
//public static class ServiceCollectionExtensions
//{
//    /// <summary>
//    /// Adds services required for Exact integration.
//    /// </summary>
//    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
//    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
//    public static IServiceCollection AddIntegrationExact(this IServiceCollection services, IConfiguration configuration)
//    {
//        Guard.NotNull(services, nameof(services));

//        var exactOptions = configuration.GetSection("ExactIntegration").Get<ExactIntegrationOptions>();
//        ArgumentNullException.ThrowIfNull(exactOptions, nameof(exactOptions));

//        services.AddOptions();
//        services.AddServices();
//        services.AddHttpClient();
//        services.AddMemoryCache();

//        return services;
//    }

//    public static void AddServices(this IServiceCollection services)
//    {
//        Guard.NotNull(services, nameof(services));

//        services.AddTransient<IExactClient, ExactClient>();
//        services.AddTransient<IExactTokenClient, ExactTokenClient>();
//        services.AddTransient<IExactTokenService, ExactAuthTokenService>();
//        services.AddTransient<IExactRefreshTokenStorageService, ExactConfigurationProvider>();
//        services.AddTransient<IUBLHelper, UBLHelper>();
//    }
//}

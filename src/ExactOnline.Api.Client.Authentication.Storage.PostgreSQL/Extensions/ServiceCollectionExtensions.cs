using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.SqlServer;
using ExactOnline.Api.Client.Authentication.Storage.SqlServer.Data;
using ExactOnline.Api.Client.Authentication.Storage.SqlServer.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExactOnlineTokenStoragePostgreSQL(this IServiceCollection services, IConfiguration configuration, ServiceLifetime dbContextLifetime = ServiceLifetime.Scoped)
    {
        services.AddOptions<ExactOnlineEntityFrameworkCoreStorageOptions>()
            .Bind(configuration.GetSection("ExactOnlinePostgreSQLStorageOptions"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddMemoryCache();

        services.AddDbContext<ExactOnlineTokenDbContext>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString(serviceProvider.GetOptions().ConnectionStringName);

            options.UseNpgsql(connectionString);
        });

        services.EnsureExactOnlineTokenTableExists();

        if (dbContextLifetime == ServiceLifetime.Scoped)
        {
            return services.AddScoped<IExactTokenStorageService, ExactTokenStorageEntityFrameworkCoreService>();
        }

        return services.AddTransient<IExactTokenStorageService, ExactTokenStorageEntityFrameworkCoreService>();
    }

    private static ExactOnlineEntityFrameworkCoreStorageOptions GetOptions(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<IOptions<ExactOnlineEntityFrameworkCoreStorageOptions>>().Value;
    }

    private static void EnsureExactOnlineTokenTableExists(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ExactOnlineTokenDbContext>();

        dbContext.Database.EnsureCreated();
        dbContext.EnsureExactOnlineTokenTableExists();
    }
}
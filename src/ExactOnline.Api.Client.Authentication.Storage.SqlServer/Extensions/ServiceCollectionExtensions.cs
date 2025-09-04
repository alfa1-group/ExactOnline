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
    public static IServiceCollection AddExactOnlineTokenStorageSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ExactOnlineEntityFrameworkCoreStorageOptions>()
            .Bind(configuration.GetSection("ExactOnlineSqlServerStorageOptions"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddMemoryCache();

        services.AddDbContext<ExactOnlineTokenDbContext>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString(serviceProvider.GetOptions().ConnectionStringName);

            options.UseSqlServer(connectionString);
        });

        using (var serviceProvider = services.BuildServiceProvider())
        {
            using var scope = serviceProvider.CreateScope();

            using var dbContext = scope.ServiceProvider.GetRequiredService<ExactOnlineTokenDbContext>();
            dbContext.Database.EnsureCreated();
            dbContext.EnsureExactOnlineTokenTableExists();
        }

        return services.AddScoped<IExactTokenStorageService, ExactTokenStorageEntityFrameworkCoreService>();
    }

    private static ExactOnlineEntityFrameworkCoreStorageOptions GetOptions(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<IOptions<ExactOnlineEntityFrameworkCoreStorageOptions>>().Value;
    }
}
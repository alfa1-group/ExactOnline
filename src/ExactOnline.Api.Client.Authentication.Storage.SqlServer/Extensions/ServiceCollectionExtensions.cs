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
        services.AddOptions<ExactOnlineSqlServerStorageOptions>()
            .Bind(configuration.GetSection(nameof(ExactOnlineSqlServerStorageOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<ExactOnlineTokenDbContext>((serviceProvider, options) =>
        {
            var storageOptions = serviceProvider.GetRequiredService<IOptions<ExactOnlineSqlServerStorageOptions>>().Value;
            options.UseSqlServer(storageOptions.ConnectionString);
        });

        using (var serviceProvider = services.BuildServiceProvider())
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ExactOnlineTokenDbContext>();
                dbContext.Database.EnsureCreated();
            }
        }

        return services.AddScoped<IExactTokenStorageService, ExactTokenStorageSqlServer>();
    }
}
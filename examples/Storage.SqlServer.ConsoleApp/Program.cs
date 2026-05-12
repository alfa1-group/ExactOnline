using Alfa1.TokenStorage.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services
            .AddSingleton(TimeProvider.System)
            .AddLogging()
            .AddTokenStorageSqlServer(context.Configuration, ServiceLifetime.Transient); // Use Transient to demonstrate concurrency handling
    });

var host = builder.Build();

var scope = host.Services.CreateScope();

var exactTokenStorageService = scope.ServiceProvider.GetRequiredService<ITokenStorageService>();

await RunAsync(async () =>
{
    _ = await exactTokenStorageService.StoreRefreshTokenAsync("initial", "r");
    var r = await exactTokenStorageService.RetrieveRefreshTokenAsync();
    Console.WriteLine("Refresh = {0}", r);

    var testRefresh = await exactTokenStorageService.StoreRefreshTokenAsync("other", "r2");
    Console.WriteLine("Refresh with invalid current = {0}", testRefresh);

    _ = await exactTokenStorageService.StoreAccessTokenAsync(null, "a", TimeSpan.FromSeconds(3));
    var a1 = await exactTokenStorageService.RetrieveAccessTokenAsync();
    Console.WriteLine("Access = {0}", a1);

    var testAccess = await exactTokenStorageService.StoreAccessTokenAsync("other", "a3", TimeSpan.FromSeconds(5));
    Console.WriteLine("Access with invalid current = {0}", testAccess);

    Console.WriteLine("Waiting 4 seconds");
    await Task.Delay(4000);

    var afterTimeout = await exactTokenStorageService.RetrieveAccessTokenAsync();
    Console.WriteLine("Access after timeout = {0}", afterTimeout);

    var currentRefreshToken = await exactTokenStorageService.RetrieveRefreshTokenAsync();
    var currentAccessToken = await exactTokenStorageService.RetrieveAccessTokenAsync();


    var refreshTasks = new List<Task<string>>();
    var accessTasks = new List<Task<string>>();
    for (var i = 0; i < 10; i++)
    {
        var refreshIdx = i;
        var refreshTask = Task.Run(async () =>
        {
            var exactTokenStorageServiceForTask = scope.ServiceProvider.GetRequiredService<ITokenStorageService>();

            await Task.Delay(Random.Shared.Next(10));
            return await exactTokenStorageServiceForTask.StoreRefreshTokenAsync(currentRefreshToken, $"r-{refreshIdx}");
        });
        refreshTasks.Add(refreshTask);

        var accessIdx = i;
        var accessTask = Task.Run(async () =>
        {
            var exactTokenStorageServiceForTask = scope.ServiceProvider.GetRequiredService<ITokenStorageService>();

            await Task.Delay(Random.Shared.Next(10));
            return await exactTokenStorageServiceForTask.StoreAccessTokenAsync(currentAccessToken, $"a-{accessIdx}", TimeSpan.FromSeconds(5));
        });
        accessTasks.Add(accessTask);
    }

    foreach (var result in await Task.WhenAll(refreshTasks))
    {
        Console.WriteLine("Concurrent refresh result = {0}", result);
    }

    foreach (var result in await Task.WhenAll(accessTasks))
    {
        Console.WriteLine("Concurrent access result = {0}", result);
    }

    Console.WriteLine("Done");

    return true;
});

scope.Dispose();

return;

static async Task<T?> RunAsync<T>(Func<Task<T>> task)
{
    try
    {
        return await task();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
    }

    return default;
}
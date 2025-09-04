using ExactOnline.Api.Client.Authentication.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services
            .AddSingleton(TimeProvider.System)
            .AddLogging()
            .AddExactOnlineTokenStorageSqlServer(context.Configuration);
    });

var host = builder.Build();

using var scope = host.Services.CreateScope();

var exactTokenStorageService = scope.ServiceProvider.GetRequiredService<IExactTokenStorageService>();

await RunAsync(async () =>
{
    _ = await exactTokenStorageService.StoreRefreshTokenAsync("r");
    var r = await exactTokenStorageService.RetrieveRefreshTokenAsync();
    Console.WriteLine("Refresh = {0}", r);

    _ = await exactTokenStorageService.StoreAccessTokenAsync("a", TimeSpan.FromSeconds(3)).ConfigureAwait(false);
    var a1 = await exactTokenStorageService.RetrieveAccessTokenAsync();
    Console.WriteLine("Access = {0}", a1);

    Console.WriteLine("Waiting 4 seconds");
    await Task.Delay(4000);

    var a2 = await exactTokenStorageService.RetrieveAccessTokenAsync();
    Console.WriteLine("Access = {0}", a2);

    return true;
});
return;

async Task<T?> RunAsync<T>(Func<Task<T>> task)
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
using ConsoleApp;
using ExactOnline.Api.Client;
using ExactOnline.Api.Client.Authentication.Interfaces;
using ExactOnline.Api.Client.Builders;
using ExactOnline.Api.Client.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services
            .AddLogging()
            .AddSingleton<IExactRefreshTokenStorageService, ExactRefreshTokenFileStorageService>()
            .AddExactOnlineKiotaAuthentication(context.Configuration);
    });

var host = builder.Build();

using var scope = host.Services.CreateScope();
var client = scope.ServiceProvider.GetRequiredService<ExactOnlineServiceClient>();

var x1 = SelectBuilder<SystemSystemMe>.Build(s => s.UserID, s => s.CurrentDivision, s => s.Email);
var x2 = SelectBuilder<SystemSystemMe>.Build(s => new { s.UserID, s.CurrentDivision, s.Email });
var orderBy = OrderByBuilder<WebhooksWebhookSubscriptions>
    .OrderBy(w => w.ID)
    .ThenByDescending(w => w.CallbackURL)
    .Build();

var me = await RunAsync(async () =>
{
    var me = await client.Api.V1.Current.Me.GetAsync(a =>
    {
        a.QueryParameters.Select = SelectBuilder<SystemSystemMe>.Build(s => s.UserID, s => s.CurrentDivision, s => s.Email);
    }).AsItem();

    Console.WriteLine($"{me!.CurrentDivision} {me!.Email}");

    return me;
});

var division = me!.CurrentDivision!.Value;

await RunAsync(async () =>
{
    Console.WriteLine("Waiting 3 seconds");
    await Task.Delay(TimeSpan.FromSeconds(3));

    return true;
});

await RunAsync(async () =>
{
    var me2 = await client.Api.V1.Current.Me.GetAsync().AsItem();
    Console.WriteLine($"After waiting: {me2?.Email}");

    return true;
});

await RunAsync(async () =>
{

    var webhookSubscriptions = await client.Api.V1[division].Webhooks.WebhookSubscriptions.GetAsync(w =>
    {
        w.QueryParameters.Top = 100;
        w.QueryParameters.Orderby = orderBy;
        w.QueryParameters.Select = SelectBuilder<WebhooksWebhookSubscriptions>.Build(t => new { t.UserID, t.CallbackURL, t.Description });
    })
    .AsItems();
    if (!webhookSubscriptions.Any())
    {
        Console.WriteLine("No WebhookSubscriptions found.");
    }

    foreach (var webhookSubscription in webhookSubscriptions)
    {
        Console.WriteLine($"Subscription ID: {webhookSubscription.ID}, CallbackURL: {webhookSubscription.CallbackURL}");
    }

    return true;
});


await RunAsync(async () =>
{
    Console.WriteLine("{0} Waiting for 10 minutes to check token refresh", DateTime.Now);
    await Task.Delay(TimeSpan.FromMinutes(10));
    var me3 = await client.Api.V1.Current.Me.GetAsync().AsItem();
    Console.WriteLine($"After waiting: {me3?.Email}");
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
        Console.WriteLine($"Error: {ex.Message}");

        return default;
    }
}
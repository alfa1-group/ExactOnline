using ExactOnline.Api.Client;
using ExactOnline.Api.Client.Builders.OrderBy;
using ExactOnline.Api.Client.Builders.Select;
using ExactOnline.Api.Client.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services
            .AddLogging()
            .AddExactOnlineTokenStorageAzureBlobs(context.Configuration)
            .AddExactOnlineKiotaAuthentication(context.Configuration);
    });

var host = builder.Build();

using var scope = host.Services.CreateScope();
var client = scope.ServiceProvider.GetRequiredService<ExactOnlineServiceClient>();

var s1 = SelectBuilder<SystemSystemMe>.Build(s => s.UserID, s => s.CurrentDivision, s => s.Email);
var s2 = SelectBuilder<SystemSystemMe>.Build(s => new { s.UserID, s.CurrentDivision, s.Email });
var orderBy = OrderByBuilder<WebhooksWebhookSubscriptions>
    .OrderBy(w => w.ID)
    .ThenByDescending(w => w.CallbackURL)
    .Build();

await RunAsync(async () =>
{
    _ = await client.Api.V1[123456].Webhooks.WebhookSubscriptionsWithId(new Guid("AE0253AA-67AB-480B-9321-F27C50AF22B7")).DeleteAsync();
    return true;
});

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
    Console.WriteLine("Getting ProjectTimeTransactions");
    var list = await client.Api.V1[division].Project.TimeTransactions.GetAsync(p =>
    {
        p.QueryParameters.Top = 10;
    }).AsItems();

    foreach (var timeTransactions in list)
    {
        Console.WriteLine($"TimeTransaction ID: {timeTransactions.ID}, Date: {timeTransactions.Date}, UserID: {timeTransactions.StartTime}");
    }

    return list;
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
    Console.WriteLine("Testing API limits per minute");
    for (var i = 0; i < 130; i++)
    {
        _ = await client.Api.V1[division].Webhooks.WebhookSubscriptions.GetAsync().AsItems();
        await Task.Delay(TimeSpan.FromMilliseconds(100));
    }

    return true;
});

await RunAsync(async () =>
{
    Console.WriteLine("{0} Waiting for 10 minutes to check token refresh", DateTime.Now);
    await Task.Delay(TimeSpan.FromMinutes(10));
    var me3 = await client.Api.V1.Current.Me.GetAsync().AsItem();
    Console.WriteLine($"After waiting 10 minutes: {me3?.Email}");
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
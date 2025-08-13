using ExactOnline.Api.Client;
using ExactOnline.Api.Client.Builders.Filter;
using ExactOnline.Api.Client.Builders.OrderBy;
using ExactOnline.Api.Client.Builders.Select;
using ExactOnline.Api.Client.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Kiota.Abstractions;

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
var filter = FilterBuilder<WebhooksWebhookSubscriptions>.Build(a => a.CallbackURL!.Equals("abc") && (a.Division > 100 || a.Created > TimeProvider.System.GetUtcNow().AddDays(-30)));
var syncFilter = SyncFilterBuilder.Build(t => t.Timestamp >= 1);

await RunAsync(async () =>
{
    await client.Api.V1[123456].Webhooks.WebhookSubscriptionsWithId(new Guid("AE0253AA-67AB-480B-9321-F27C50AF22B7")).DeleteAsync();
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

Console.WriteLine("Waiting 3 seconds");
await Task.Delay(TimeSpan.FromSeconds(3));

await RunAsync(async () =>
{
    var meTop = await client.Api.V1.Current.Me.GetAsync(m => m.QueryParameters.Top = 1).AsItem();
    Console.WriteLine($"After waiting: {meTop?.Email}");

    return true;
});

await RunAsync(async () =>
{
    Console.WriteLine("Getting Sync.Project.TimeCostTransactions : 400 error");
    _ = await client.Api.V1[division].Sync.Project.TimeCostTransactions.GetAsync(p =>
    {
        p.QueryParameters.Filter = syncFilter;
    }).AsItems();

    return false;
});

await RunAsync(async () =>
{
    Console.WriteLine("Post WebHook");
    var postResult = await client.Api.V1[division].Webhooks.WebhookSubscriptions.PostAsync(new WebhooksWebhookSubscriptionsPost
    {
        CallbackURL = "https://mstack.nl",
        Topic = "StockPositions"
    }).AsItem();

    Console.WriteLine($"Post WebHook ID: {postResult.ID}");

    return true;
});

await RunAsync(async () =>
{
    var webhookSubscriptions = await client.Api.V1[division].Webhooks.WebhookSubscriptions.GetAsync(w =>
        {
            w.QueryParameters.Top = 100;
            w.QueryParameters.Orderby = orderBy;
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
    Console.WriteLine("Getting ProjectTimeTransactions");
    var list = await client.Api.V1[division].Project.TimeTransactions.GetAsync(p =>
    {
        p.QueryParameters.Top = 10;
        p.QueryParameters.Filter = FilterBuilder<ProjectTimeTransactions>.Build(t => t.Created >= TimeProvider.System.GetUtcNow().AddDays(-30));
    }).AsItems();

    foreach (var tt in list)
    {
        Console.WriteLine($"TimeTransaction ID: {tt.ID}, Created: {tt.Created}");
    }

    return list;
});

await RunAsync(async () =>
{
    Console.WriteLine("Getting Sync.Project.TimeCostTransactions");
    var list = await client.Api.V1[division].Sync.Project.TimeCostTransactions.GetAsync(p =>
    {
        p.QueryParameters.Top = 1; // Must be 1 !
        p.QueryParameters.Filter = syncFilter;
    }).AsItems();

    foreach (var tt in list)
    {
        Console.WriteLine($"Sync.Project.TimeCostTransactions ID: {tt.ID}, Type: {tt.Type}, Created: {tt.Created}");
    }

    return list;
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
    catch (ODataError odata)
    {
        Console.WriteLine($"ODataError: {odata.Message}");
        Console.WriteLine($"Error Code: {odata.Error?.Code}");
        Console.WriteLine($"Error Message: {odata.Error?.Message?.Value}");
    }
    catch (ApiException apiEx)
    {
        Console.WriteLine($"ApiException: {apiEx.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
    }

    return default;
}
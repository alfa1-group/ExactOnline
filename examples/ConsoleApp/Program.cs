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

var x1 = SelectBuilder.Build<SystemSystemMe>(s => s.UserID, s => s.CurrentDivision, s => s.Email);
var x2 = SelectBuilder.Build<SystemSystemMe>(s => new { s.UserID, s.CurrentDivision, s.Email });

var me = await client.Api.V1.Current.Me.GetAsync(a =>
{
    a.QueryParameters.Select = SelectBuilder.Build<SystemSystemMe>(s => s.UserID, s => s.CurrentDivision, s => s.Email);
}).AsItem();
var division = me!.CurrentDivision!.Value;
Console.WriteLine($"{division} {me.Email}");

Console.WriteLine("Waiting 3 seconds");
await Task.Delay(TimeSpan.FromSeconds(3));

var me2 = await client.Api.V1.Current.Me.GetAsync().AsItem();
Console.WriteLine($"After waiting: {me2?.Email}");

var webhookSubscriptions = await client.Api.V1[division].Webhooks.WebhookSubscriptions.GetAsync(w =>
{
    w.QueryParameters.Top = 100;
    w.QueryParameters.Orderby = $"{nameof(WebhooksWebhookSubscriptions.ID)} desc";
    w.QueryParameters.Select = SelectBuilder.Build<WebhooksWebhookSubscriptions>(t => new { t.UserID, t.CallbackURL, t.Description });
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

Console.WriteLine("{0} Waiting for 10 minutes to check token refresh", DateTime.Now);
await Task.Delay(TimeSpan.FromMinutes(10));
var me3 = await client.Api.V1.Current.Me.GetAsync().AsItem();
Console.WriteLine($"After waiting: {me3?.Email}");
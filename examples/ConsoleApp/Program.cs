using ConsoleApp;
using ExactOnline.Api.Client;
using ExactOnline.Api.Client.Authentication.Interfaces;
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

var me = await client.Api.V1.Current.Me.GetAsync(a =>
{
    // a.QueryParameters.Select = "ID,Email,FirstName,LastName";
}).AsItem();
var division = me!.CurrentDivision!.Value;
Console.WriteLine($"{division} {me.Email}");

Console.WriteLine("Waiting 3 seconds");
await Task.Delay(TimeSpan.FromSeconds(3));

var me2 = await client.Api.V1.Current.Me.GetAsync().AsItem();
Console.WriteLine($"After waiting: {me2?.Email}");

var subscriptions = await client.Api.V1[division].Webhooks.WebhookSubscriptions.GetAsync().AsResults();
if (!subscriptions.Any())
{
    Console.WriteLine("No WebhookSubscriptions found.");
}
foreach (var subscription in subscriptions)
{
    Console.WriteLine($"Subscription ID: {subscription.ID}, CallbackURL: {subscription.CallbackURL}");
}

Console.WriteLine("{0} Waiting for 11 minutes to check token refresh", DateTime.Now);
await Task.Delay(TimeSpan.FromMinutes(11));
var me3 = await client.Api.V1.Current.Me.GetAsync().AsItem();
Console.WriteLine($"After waiting: {me3?.Email}");
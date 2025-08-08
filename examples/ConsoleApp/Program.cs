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
            .AddExactOnlineKiotaAuthenticated(context.Configuration);
    });

var host = builder.Build();


using var scope = host.Services.CreateScope();
var client = scope.ServiceProvider.GetRequiredService<ExactOnlineServiceClient>();

var meResponse = await client.Api.V1.Current.Me.GetAsync(a =>
{
    // a.QueryParameters.Select = "ID,Email,FirstName,LastName";
});
var me = meResponse.AsItem()!;
var division = me.CurrentDivision!.Value;
Console.WriteLine($"{division} {me.Email}");

var subscriptions = await client.Api.V1[division].Webhooks.WebhookSubscriptions.GetAsync();
if (!subscriptions.AsResults().Any())
{
    Console.WriteLine("No WebhookSubscriptions found.");
}
foreach (var subscription in subscriptions.AsResults())
{
    Console.WriteLine($"Subscription ID: {subscription.ID}, Webhook URL: {subscription.CallbackURL}");
}
using ConsoleApp;
using ExactOnline.Api.Client;
using ExactOnline.Api.Client.Authentication.Interfaces;
using ExactOnline.Api.Client.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simple.OData.Client;

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
var me = meResponse?.ToItem();
Console.WriteLine($"Hello {me?.CurrentDivision} {me?.Email}");
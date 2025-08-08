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
    a.QueryParameters.Select = "ID,Email,DisplayName,FirstName,LastName,Language,Locale,TimeZone";
});
var me = meResponse?.ToItem();

var accountancyAccountInvolvedAccountsResponse = await client.Api.V1["abc"].Accountancy.AccountInvolvedAccounts.GetAsync(r =>
{
    r.QueryParameters.Filter = "ID eq guid'3fa85f64-5717-4562-b3fc-2c963f66afa6'";
});
var accountancyAccountInvolvedAccounts = accountancyAccountInvolvedAccountsResponse?.ToResults();

await client.Api.V1["abc"].Accountancy.AccountInvolvedAccountsWithId(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")).PutAsync(new AccountancyAccountInvolvedAccounts(), r =>
{
});

int xxxx = 0;

var settings = new ODataClientSettings
{
    BaseUri = new Uri("https://start.exactonline.nl/api/v1/"),
    BeforeRequestAsync = (r) =>
    {
        return Task.CompletedTask;
    }
};

var oclient = new ODataClient("http://packages.nuget.org/v1/FeedService.svc/");

var packages = await oclient
    .For<SystemSystemMe>()
    .Filter(x => x.Title == "Simple.OData.Client")
    .FindEntryAsync();
//foreach (var package in packages)
//{
//    Console.WriteLine(package.Title);
//}
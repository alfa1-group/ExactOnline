using ConsoleApp;
using ExactOnline.Api.Client;
using ExactOnline.Api.Client.Authentication.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Kiota.Serialization;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services
            .AddLogging()
            .AddSingleton<IExactRefreshTokenStorageService, ExactRefreshTokenFileStorageService>()
            .AddExactOnlineAuthenticatedClient(context.Configuration);
    });

var host = builder.Build();


using var scope = host.Services.CreateScope();
var client = scope.ServiceProvider.GetRequiredService<ExactOnlineServiceClient>();

var me = await client.Api.V1.Current.Me.GetAsync();

//var accountancyAccountInvolvedAccounts = await client.Api.V1["abc"].Accountancy.AccountInvolvedAccounts.GetAsync(r =>
//{
//    r.QueryParameters.Filter = "ID eq guid'3fa85f64-5717-4562-b3fc-2c963f66afa6'";
//});

//await client.Api.V1["abc"].Accountancy.AccountInvolvedAccountsWithId(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")).PutAsync(new AccountancyAccountInvolvedAccounts(), r =>
//{
//});
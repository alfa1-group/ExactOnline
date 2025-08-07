using CVMatch.ConsoleApp;
using ExactOnline.Api.Client;
using ExactOnline.Api.Client.Models;

var authenticationProvider = new ExampleTokenAuthenticationProvider();

var client = new ExactOnlineServiceClientProxy(new ExactOnlineServiceClient(authenticationProvider));

var me = await client.Api.V1.Current.Me.GetAsync();

var accountancyAccountInvolvedAccounts = await client.Api.V1["abc"].Accountancy.AccountInvolvedAccounts.GetAsync(r =>
{
    r.QueryParameters.Filter = "ID eq guid'3fa85f64-5717-4562-b3fc-2c963f66afa6'";
});

await client.Api.V1["abc"].Accountancy.AccountInvolvedAccountsWithId(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")).PutAsync(new AccountancyAccountInvolvedAccounts(), r =>
{
});
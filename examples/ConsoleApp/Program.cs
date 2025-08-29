using ExactOnline.Api.Client;
using ExactOnline.Api.Client.Authentication.Options;
using ExactOnline.Api.Client.Builders.Filter;
using ExactOnline.Api.Client.Builders.OrderBy;
using ExactOnline.Api.Client.Builders.Select;
using ExactOnline.Api.Client.Extensions;
using ExactOnline.Api.Client.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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

var options = scope.ServiceProvider.GetRequiredService<IOptions<ExactOnlineOptions>>();
var isDevelopment = !options.Value.ClientId.StartsWith("e4a2");

var client = scope.ServiceProvider.GetRequiredService<ExactOnlineServiceClient>();

var someLogisticsItems = SelectBuilder<LogisticsItem>.Build(l => l.Description, l => l.FreeTextField01);
var selectAllLogisticsItems = SelectBuilder<LogisticsItem>.Build();
var filterLogisticsItems = FilterBuilder<LogisticsItem>.Build(l => l.Description == "abc" && l.FreeTextField01 == "tst");
var filterPayrollEmployees = FilterBuilder<PayrollEmployee>.Build(p => p.Email == "test+dev@abc.com" && p.FullName == null);
var s1 = SelectBuilder<SystemSystemMe>.Build(s => s.UserID, s => s.CurrentDivision, s => s.Email);
var s2 = SelectBuilder<SystemSystemMe>.Build(s => new { s.UserID, s.CurrentDivision, s.Email });
var orderBy = OrderByBuilder<WebhooksWebhookSubscription>
    .OrderBy(w => w.ID)
    .ThenByDescending(w => w.CallbackURL)
    .Build();
var filter = FilterBuilder<WebhooksWebhookSubscription>.Build(a => a.CallbackURL!.Equals("abc") && (a.Division > 100 || a.Created > TimeProvider.System.GetUtcNow().AddDays(-30)));
var selectAll = SelectBuilder<SyncProjectTimeCostTransaction>.Build();
var usersUserRolesPerDivisionSelectAll = SelectBuilder<UsersUserRolesPerDivision>.Build();

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

var division = isDevelopment ? me!.CurrentDivision!.Value : scope.ServiceProvider.GetRequiredService<IConfiguration>().GetValue<int>("Division");

await RunAsync(async () =>
{
    // test multiple tasks in parallel
    var task1 = client.Api.V1[division].Users.Users.GetAsync().AsItems();

    var task2 = client.Api.V1[division].Users.Users.GetAsync().AsItems();

    var tsTask = client.Api.V1[division].Read.Sync.Sync.SyncTimestamp.GetAsync(s =>
    {
        s.QueryParameters.Modified = new DateTimeOffset(2025, 8, 1, 0, 0, 0, TimeSpan.Zero).ToODataFormat();
        s.QueryParameters.EndPoint = "TimeCostTransactions".ToODataFormat();
    }).AsItem();

    await Task.WhenAll(task1, task2, tsTask);

    Console.WriteLine($"Task1 Users: {task1.Result.Count}, Task2 Users: {task2.Result.Count}, Timestamp: {tsTask.Result?.Modified} {tsTask.Result?.TimeStampAsBigInt} {tsTask.Result?.API}");

    return true;
});

//var users = await RunAsync(async () =>
//{
//    Console.WriteLine("Getting Users/Users");
//    var list = await client.Api.V1[division].Users.Users.GetAllAsync().AsItems();

//    foreach (var x in list)
//    {
//        Console.WriteLine($"Fullname: {x.FullName}, UserID: {x.UserID}");
//    }

//    return list;
//});

//var usersRoles = await RunAsync(async () =>
//{
//    Console.WriteLine("Getting Users/UserRoles");
//    var list = await client.Api.V1[division].Users.UserRoles.GetAllAsync().AsItems();

//    foreach (var x in list)
//    {
//        Console.WriteLine($"UserID: {x.UserID}, Role: {x.Role}, RoleLevel: {x.RoleLevel}");
//    }

//    return list;
//});

await RunAsync(async () =>
{
    Console.WriteLine("Getting Users/UserRolesPerDivision");
    var list = await client.Api.V1[division].Users.UserRolesPerDivision.GetAllAsync(b =>
    {
        b.QueryParameters.Select = usersUserRolesPerDivisionSelectAll;
        b.QueryParameters.Filter = FilterBuilder<UsersUserRolesPerDivision>.Build(u => u.UserID == new Guid("..."));
    }).AsItems();

    foreach (var x in list)
    {
        Console.WriteLine($"UserID: {x.UserID}, Role: {x.Role}, RoleLevel: {x.RoleLevel}");
    }

    return list;
});

var ts = await RunAsync(async () =>
{
    Console.WriteLine("Getting Sync/SyncTimestamp");
    var ts = await client.Api.V1[division].Read.Sync.Sync.SyncTimestamp.GetAsync(s =>
    {
        s.QueryParameters.Modified = new DateTimeOffset(2025, 8, 1, 0, 0, 0, TimeSpan.Zero).ToODataFormat();
        s.QueryParameters.EndPoint = "TimeCostTransactions".ToODataFormat();
    }).AsItem();

    Console.WriteLine($"Timestamp: {ts?.Modified} {ts?.TimeStampAsBigInt} {ts?.API}");

    return ts!.TimeStampAsBigInt;
});

var syncFilter = TimestampFilterBuilder.Build(t => t.Timestamp >= ts);

var all = await RunAsync(async () =>
{
    Console.WriteLine("Getting System Divisions - GetAll");
    var list = await client.Api.V1[division].System.Divisions.GetAllAsync().AsItems();

    foreach (var x in list)
    {
        Console.WriteLine($"Division: {x.Code}, {x.Description}");
    }

    return list;
});

await RunAsync(async () =>
{
    Console.WriteLine("Getting Sync Deleted - GetAll");
    var list = await client.Api.V1[division].Sync.Deleted.GetAllAsync(x =>
    {
        x.QueryParameters.Filter = syncFilter;
        x.QueryParameters.Select = SelectBuilder<SyncDeleted>.Build();
    }).AsItems();

    foreach (var x in list)
    {
        Console.WriteLine($"Sync Deleted: {x.ID}, {x.EntityKey} {x.EntityType}");
    }

    return list;
});

await RunAsync(async () =>
{
    Console.WriteLine("Getting Sync Project Projects - GetAll");
    var list = await client.Api.V1[division].Sync.Project.Projects.GetAllAsync(a =>
    {
        a.QueryParameters.Filter = TimestampFilterBuilder.Build(t => t.Timestamp >= 1);
        a.QueryParameters.Select = SelectBuilder<SyncProjectProject>.Build();
    }).AsItems();

    foreach (var a in list)
    {
        Console.WriteLine($"Sync Project Project: {a.ID}, Created: {a.Created}");
    }

    return list;
});

await RunAsync(async () =>
{
    Console.WriteLine("Getting Sync Crm Accounts - GetAll");
    var list = await client.Api.V1[division].Sync.CRM.Accounts.GetAllAsync(a =>
    {
        a.QueryParameters.Filter = TimestampFilterBuilder.Build(t => t.Timestamp >= 1);
        a.QueryParameters.Select = SelectBuilder<SyncCRMAccount>.Build();
    }).AsItems();

    foreach (var a in list)
    {
        Console.WriteLine($"Sync Crm Account ID: {a.ID}, Created: {a.Created}");
    }

    return list;
});

await RunAsync(async () =>
{
    Console.WriteLine("Getting Sync Payroll Employees - GetAll");
    var list = await client.Api.V1[division].Sync.Payroll.Employees.GetAllAsync(a =>
    {
        a.QueryParameters.Filter = TimestampFilterBuilder.Build(t => t.Timestamp >= 1);
        a.QueryParameters.Select = SelectBuilder<SyncPayrollEmployee>.Build();
    }).AsItems();

    foreach (var a in list)
    {
        Console.WriteLine($"Sync Payroll Employee ID: {a.ID}, Created: {a.Created}");
    }

    return list;
});

await RunAsync(async () =>
{
    Console.WriteLine("Getting Sync Logistics Items - GetAll");
    var list = await client.Api.V1[division].Sync.Logistics.Items.GetAllAsync(a =>
    {
        a.QueryParameters.Filter = TimestampFilterBuilder.Build(t => t.Timestamp >= 1);
        a.QueryParameters.Select = SelectBuilder<SyncLogisticsItem>.Build();
    }).AsItems();

    foreach (var a in list)
    {
        Console.WriteLine($"Sync Logistics Item ID: {a.ID}, Created: {a.Created}");
    }

    return list;
});

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

var projectTimeTransaction = await RunAsync(async () =>
{
    Console.WriteLine("Getting ProjectTimeTransactions");
    var list = await client.Api.V1[division].Project.TimeTransactions.GetAsync(p =>
    {
        p.QueryParameters.Top = 10;
        p.QueryParameters.Filter = FilterBuilder<ProjectTimeTransaction>.Build(t => t.Created >= TimeProvider.System.GetUtcNow().AddDays(-30));
    }).AsItems();

    foreach (var tt in list)
    {
        Console.WriteLine($"TimeTransaction ID: {tt.ID}, Created: {tt.Created}");
    }

    return list.FirstOrDefault();
});

await RunAsync(async () =>
{
    Console.WriteLine("Getting ProjectTimeTransactions - GetAll");
    var list = await client.Api.V1[division].Project.TimeTransactions.GetAllAsync(p =>
    {
        p.QueryParameters.Top = 70;
        p.QueryParameters.Filter = FilterBuilder<ProjectTimeTransaction>.Build(t => t.Created >= TimeProvider.System.GetUtcNow().AddDays(-30));
    }).AsItems();

    foreach (var tt in list)
    {
        Console.WriteLine($"TimeTransaction ID: {tt.ID}, Created: {tt.Created}");
    }

    return list.FirstOrDefault();
});

await RunAsync(async () =>
{
    if (!isDevelopment)
    {
        Console.WriteLine("Skipping Project.TimeTransaction tests in production environment.");
        return false;
    }

    Console.WriteLine("Updating Project.TimeTransaction with ID {0}", projectTimeTransaction?.ID);
    await client.Api.V1[division].Project.TimeTransactionsWithId(projectTimeTransaction?.ID).PutAsync(new ProjectTimeTransactionPut
    {
        Notes = "Updated via API at " + TimeProvider.System.GetUtcNow().ToString("o")
    });

    return true;
});

await RunAsync(async () =>
{
    Console.WriteLine("Getting Sync.Project.TimeCostTransactions - top 1");
    var list = await client.Api.V1[division].Sync.Project.TimeCostTransactions.GetAsync(p =>
    {
        p.QueryParameters.Top = 1;
        p.QueryParameters.Filter = syncFilter;
    }).AsItems();

    foreach (var tt in list)
    {
        Console.WriteLine($"Sync.Project.TimeCostTransactions TS: {tt.Timestamp} ID: {tt.ID}, Type: {tt.Type}, Created: {tt.Created}");
    }

    return list;
});

await RunAsync(async () =>
{
    Console.WriteLine("Getting Sync.Project.TimeCostTransactions - select");
    var list = await client.Api.V1[division].Sync.Project.TimeCostTransactions.GetAsync(p =>
    {
        p.QueryParameters.Select = SelectBuilder<SyncProjectTimeCostTransaction>.Build(t => t.ID, t => t.Timestamp, t => t.Type, t => t.Created);
        p.QueryParameters.Filter = syncFilter;
    }).AsItems();

    foreach (var tt in list)
    {
        Console.WriteLine($"Sync.Project.TimeCostTransactions TS: {tt.Timestamp} ID: {tt.ID}, Type: {tt.Type}, Created: {tt.Created}");
    }

    return list;
});

await RunAsync(async () =>
{
    Console.WriteLine("Getting Sync.Project.TimeCostTransactions - ALL");
    var list = await client.Api.V1[division].Sync.Project.TimeCostTransactions.GetAllAsync(p =>
    {
        p.QueryParameters.Top = 1100;
        p.QueryParameters.Select = SelectBuilder<SyncProjectTimeCostTransaction>.Build();
        p.QueryParameters.Filter = syncFilter;
    }).AsItems();

    foreach (var tt in list)
    {
        Console.WriteLine($"Sync.Project.TimeCostTransactions TS: {tt.Timestamp} ID: {tt.ID}, Type: {tt.Type}, Created: {tt.Created}");
    }

    return list;
});

await RunAsync(async () =>
{
    Console.WriteLine("Getting Sync.Project.TimeCostTransactions - select & top");
    var list = await client.Api.V1[division].Sync.Project.TimeCostTransactions.GetAsync(p =>
    {
        p.QueryParameters.Top = 2;
        p.QueryParameters.Select = SelectBuilder<SyncProjectTimeCostTransaction>.Build(t => t.ID, t => t.Timestamp, t => t.Type, t => t.Created);
        p.QueryParameters.Filter = syncFilter;
    }).AsItems();

    foreach (var tt in list)
    {
        Console.WriteLine($"Sync.Project.TimeCostTransactions TS: {tt.Timestamp} ID: {tt.ID}, Type: {tt.Type}, Created: {tt.Created}");
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
# ExactOnline
Some projects to access the Exact Online REST API using C#.


## ExactOnline.Api.Client
A Kiota generated C# client for Exact Online to access the REST API.

[![NuGet Badge](https://img.shields.io/nuget/v/ExactOnline.Api.Client)](https://www.nuget.org/packages/ExactOnline.Api.Client)


### Code Example
``` c#
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

var me = await client.Api.V1.Current.Me.GetAsync().AsItem();
Console.WriteLine($"{me.CurrentDivision} {me.Email}");
```


## Authentication using Refresh and AccessToken

### Getting
For getting an AccessToken (based on RefreshToken), these two projects are used:

| Package | NuGet |
| :- | :- |
| ExactOnline.Api.Client.Authentication | [![NuGet Badge](https://img.shields.io/nuget/v/ExactOnline.Api.Client.Authentication)](https://www.nuget.org/packages/ExactOnline.Api.Client.Authentication)
| ExactOnline.Api.Client.Authentication.Kiota | [![NuGet Badge](https://img.shields.io/nuget/v/ExactOnline.Api.Client.Authentication.Kiota)](https://www.nuget.org/packages/ExactOnline.Api.Client.Authentication.Kiota)

Note that the `ExactOnline.Api.Client.Authentication` can also be used when not using the Kiota generated client, but it is required for the `ExactOnline.Api.Client.Authentication.Kiota` package.

### Storing
For storing and retrieving the RefreshToken and AccessToken, this project used:
- [Alfa1.TokenStorage](https://github.com/alfa1-group/Alfa1.TokenStorage)

---


## HowTo
In case the Exact Online REST interface is changed, you can regenerate the client using the following commands:

### Generate exactonline-openapi
This dotnet tool generates a OpenApi.json file. Idea based on [exact-online-meta-data-tool](https://github.com/DannyvdSluijs/exact-online-meta-data-tool).

#### Installation
``` cmd
dotnet tool install --global ExactOnline.OpenApiGenerator
```

#### Usage
``` cmd
ExactOnline.OpenApiGenerator --destination "../../../../../resources/exactonline-openapi.json"
```

### Generate ExactOnline.Api.Client + extension methods
``` cmd
./kiota-generate.ps1
```
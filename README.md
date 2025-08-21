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


## ExactOnline.Api.Client.Authentication
Implementation of the OAuth authentication for Exact Online.
It uses the `ExactOnline.Api.Client.Authentication.Abstractions` interfaces package to store the Refresh Token in a storage.

[![NuGet Badge](https://img.shields.io/nuget/v/ExactOnline.Api.Client.Authentication)](https://www.nuget.org/packages/ExactOnline.Api.Client.Authentication)


## ExactOnline.Api.Client.Authentication.Kiota
Contains an implementation of the `IAuthenticationProvider` interface for Kiota, which is used to authenticate requests to the Exact Online API.

[![NuGet Badge](https://img.shields.io/nuget/v/ExactOnline.Api.Client.Authentication.Kiota)](https://www.nuget.org/packages/ExactOnline.Api.Client.Authentication.Kiota)


## ExactOnline.Api.Client.Authentication.Abstractions
An interface `IExactTokenStorageService` which defines how to store and retrieve the Refresh and Access Tokens.




This interface is implemented by several packages, like:

| Package | NuGet |
| :- | :- |
| ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs | [![NuGet Badge](https://img.shields.io/nuget/v/ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs)](https://www.nuget.org/packages/ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs)
| ExactOnline.Api.Client.Authentication.Storage.FileSystem | [![NuGet Badge](https://img.shields.io/nuget/v/ExactOnline.Api.Client.Authentication.Storage.FileSystem)](https://www.nuget.org/packages/ExactOnline.Api.Client.Authentication.Storage.FileSystem)
| ExactOnline.Api.Client.Authentication.Storage.SqlServer | [![NuGet Badge](https://img.shields.io/nuget/v/ExactOnline.Api.Client.Authentication.Storage.SqlServer)](https://www.nuget.org/packages/ExactOnline.Api.Client.Authentication.Storage.SqlServer)
| ExactOnline.Api.Client.Authentication.Storage.PostgreSQL | [![NuGet Badge](https://img.shields.io/nuget/v/ExactOnline.Api.Client.Authentication.Storage.PostgreSQL)](https://www.nuget.org/packages/ExactOnline.Api.Client.Authentication.Storage.PostgreSQL)


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
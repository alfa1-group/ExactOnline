using System.Reflection;
using ExactOnline.OpenApiGenerator;
using ExactOnline.OpenApiGenerator.HtmlDocumentLoaders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var cts = new CancellationTokenSource();

AppDomain.CurrentDomain.ProcessExit += (_, _) =>
{
    cts.Cancel();
};

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

AppDomain.CurrentDomain.UnhandledException += (_, _) =>
{
    cts.Cancel();
};

Console.ForegroundColor = ConsoleColor.White;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        //config.AddCommandLine(args);
        //config.AddEnvironmentVariables();
        //config.AddJsonFile("appsettings.json", optional: false);
        //config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true);
        //config.AddEnvironmentVariables();
        //config.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<OpenApiBuilderService>();
        services.AddSingleton<PuppeteerHtmlLoader>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    });

using var host = builder.Build();

var service = host.Services.GetRequiredService<OpenApiBuilderService>();
return await service.InvokeAsync(cts.Token);
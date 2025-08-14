using ExactOnline.OpenApiGenerator;
using ExactOnline.OpenApiGenerator.HtmlDocumentLoaders;
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
    .ConfigureServices((_, services) =>
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
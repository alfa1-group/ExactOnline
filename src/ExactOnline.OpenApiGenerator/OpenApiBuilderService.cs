using ExactOnline.OpenApiGenerator.Crawlers;
using ExactOnline.OpenApiGenerator.HtmlDocumentLoaders;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi;
using MonkeyCache.FileStore;
using ShellProgressBar;

namespace ExactOnline.OpenApiGenerator;

internal class OpenApiBuilderService
{
    private const string MainPage = "https://start.exactonline.nl/docs/HlpRestAPIResources.aspx";
    private const string DetailsPage = "https://start.exactonline.nl/docs/HlpRestAPIResourcesDetails.aspx";
    private readonly int _detailsPageStringLength = DetailsPage.Length + 6;

    private readonly PuppeteerHtmlLoader _puppeteerHtmlLoader;
    private readonly string _destination;
    private readonly bool _useCache;
    private readonly bool _clearCache;

    public OpenApiBuilderService(IConfiguration configuration, PuppeteerHtmlLoader puppeteerHtmlLoader)
    {
        _puppeteerHtmlLoader = puppeteerHtmlLoader;

        _destination = configuration.GetValue<string>("destination") ?? "exactonline-openapi.json";
        _useCache = configuration.GetValue<bool?>("cache") ?? true;
        _clearCache = configuration.GetValue<bool?>("clearcache") ?? false;

        Barrel.ApplicationId = "ExactOnline.OpenApiGenerator";
    }

    public async Task<int> InvokeAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("ExactOnline OpenApiGenerator");
        Console.WriteLine("By Stef Heyenrath");
        Console.WriteLine();

        if (_clearCache)
        {
            Console.WriteLine("Clearing cache");
            Barrel.Current.EmptyAll();
        }

        var pages = (await MainPageCrawler.ExtractEndpointUrlsAsync(MainPage))
            //.Where(x =>
            //    x.Contains("SystemSystemMe") ||
            //    x.Contains("WebhooksWebhookSubscriptions") ||
            //    x.Contains("TimeTransactions") ||
            //    x.Contains("SyncProjectTimeCostTransactions")
            //)
            .ToList();

        var crawler = new EndpointCrawler(_puppeteerHtmlLoader, pages, _useCache);

        var options = new ProgressBarOptions
        {
            ProgressCharacter = '─',
            ProgressBarOnBottom = true,
            ForegroundColor = ConsoleColor.Yellow
        };

        OpenApiDocument openApiDoc;
        using (var progressBar = new ProgressBar(pages.Count, "Processing documentation", options))
        {
            openApiDoc = await crawler.CrawlAndProcessAsync(endpoint =>
            {
                progressBar.Tick($"Processing {endpoint.Substring(_detailsPageStringLength)}");
            }, cancellationToken);
        }

        if (cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("Operation cancelled by user.");
            return 1;
        }

        crawler.AddExtra(openApiDoc);

        await using var outputStream = File.CreateText(_destination);
        var writer = new OpenApiJsonWriter(outputStream);
        openApiDoc.SerializeAsV3(writer);

        Console.WriteLine();
        Console.WriteLine($"Written OpenApi file '{_destination}'");

        return 0;
    }
}
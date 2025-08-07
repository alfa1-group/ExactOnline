using ExactOnline.OpenApiGenerator.Crawlers;
using Microsoft.OpenApi;
using ShellProgressBar;

namespace ExactOnline.OpenApiGenerator;

public class OpenApiBuilderService
{
    private const string MainPage = "https://start.exactonline.nl/docs/HlpRestAPIResources.aspx";
    private const string DetailsPage = "https://start.exactonline.nl/docs/HlpRestAPIResourcesDetails.aspx";
    private readonly int _detailsPageStringLength = DetailsPage.Length + 6;

    public async Task<int> InvokeAsync(string[] args)
    {
        var destination = "exactonline-openapi.json";
        for (int i = 0; i < args.Length; i++)
        {
            if ((args[i] == "--destination" || args[i] == "-d") && i + 1 < args.Length)
            {
                destination = args[i + 1];
            }
        }

        Console.WriteLine("ExactOnline OpenApiGenerator");
        Console.WriteLine("By Stef Heyenrath");
        Console.WriteLine();

        var pages = (await MainPageCrawler.ExtractEndpointUrlsAsync(MainPage)).Take(999).ToList();

        var options = new ProgressBarOptions
        {
            ProgressCharacter = '─',
            ProgressBarOnBottom = true,
            ForegroundColor = ConsoleColor.Yellow
        };

        OpenApiDocument openApiDoc;
        using (var progressBar = new ProgressBar(pages.Count, "Processing documentation", options))
        {
            var crawler = new EndpointCrawler(pages);
            openApiDoc = await crawler.CrawlAsync(endpoint =>
            {
                progressBar.Tick($"Processing {endpoint.Substring(_detailsPageStringLength)}");
            });
        }

        await using var outputStream = File.CreateText(destination);
        var writer = new OpenApiJsonWriter(outputStream);
        openApiDoc.SerializeAsV3(writer);

        Console.WriteLine();
        Console.WriteLine($"Written OpenApi file '{destination}'");

        return 0;
    }
}
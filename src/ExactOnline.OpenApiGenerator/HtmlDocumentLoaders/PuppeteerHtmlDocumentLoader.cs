using HtmlAgilityPack;
using PuppeteerSharp;

namespace ExactOnline.OpenApiGenerator.HtmlDocumentLoaders;

internal class PuppeteerHtmlDocumentLoader : IHtmlDocumentLoader
{
    private readonly Lazy<Task<IBrowser>> _browserAsLazy = new Lazy<Task<IBrowser>>(async () =>
    {
        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        return await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
    });

    public async Task<HtmlDocument> LoadAsync(string url, CancellationToken cancellationToken)
    {
        var browser = await _browserAsLazy.Value;

        // Load the HTML document from the web page
        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(url);
        var content = await page.GetContentAsync();

        // Parse content using HtmlAgilityPack
        var doc = new HtmlDocument();
        doc.LoadHtml(content);

        return doc;
    }
}
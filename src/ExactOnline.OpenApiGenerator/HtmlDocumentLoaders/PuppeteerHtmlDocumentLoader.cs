using HtmlAgilityPack;
using PuppeteerSharp;

namespace ExactOnline.OpenApiGenerator.HtmlDocumentLoaders;

internal class PuppeteerHtmlDocumentLoader : IAsyncDisposable
{
    private readonly Lazy<Task<IBrowser>> _browserAsLazy = new(async () =>
    {
        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        return await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
    });

    public async Task<IDictionary<HttpMethod, HtmlDocument>> LoadAsync(string url, CancellationToken cancellationToken)
    {
        var browser = await _browserAsLazy.Value;

        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(url);

        var docs = new Dictionary<HttpMethod, HtmlDocument>
        {
            { HttpMethod.Get, await LoadPageAsDocumentAsync(page) }
        };

        foreach (var httpMethod in new[] { HttpMethod.Post, HttpMethod.Put, HttpMethod.Delete })
        {
            var radioButton = await page.QuerySelectorAsync($"input[type='radio'][name='supportedmethods'][value='{httpMethod.ToString().ToUpperInvariant()}']");
            if (radioButton == null)
            {
                continue;
            }

            await radioButton.ClickAsync();
            await page.WaitForNetworkIdleAsync();
            docs.Add(httpMethod, await LoadPageAsDocumentAsync(page));
        }

        return docs;
    }

    private static async Task<HtmlDocument> LoadPageAsDocumentAsync(IPage page)
    {
        var content = await page.GetContentAsync();

        var doc = new HtmlDocument();
        doc.LoadHtml(content);
        return doc;
    }

    public async ValueTask DisposeAsync()
    {
        var browser = await _browserAsLazy.Value;

        await browser.CloseAsync();
        await browser.DisposeAsync();
    }
}
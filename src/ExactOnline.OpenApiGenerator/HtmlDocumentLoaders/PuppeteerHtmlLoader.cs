using PuppeteerSharp;

namespace ExactOnline.OpenApiGenerator.HtmlDocumentLoaders;

internal class PuppeteerHtmlLoader : IAsyncDisposable
{
    private readonly Lazy<Task<IBrowser>> _browserAsLazy = new(async () =>
    {
        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        return await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
    });

    public async Task<IDictionary<HttpMethod, string>> LoadAsync(string url, CancellationToken cancellationToken)
    {
        var browser = await _browserAsLazy.Value;

        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(url);

        var contentDictionary = new Dictionary<HttpMethod, string>
        {
            { HttpMethod.Get, await page.GetContentAsync() }
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
            contentDictionary.Add(httpMethod, await page.GetContentAsync());
        }

        return contentDictionary;
    }

    public async ValueTask DisposeAsync()
    {
        var browser = await _browserAsLazy.Value;

        await browser.CloseAsync();
        await browser.DisposeAsync();
    }
}
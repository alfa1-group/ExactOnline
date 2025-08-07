using HtmlAgilityPack;

namespace ExactOnline.OpenApiGenerator.HtmlDocumentLoaders;

internal class HtmlAgilityDocumentLoader : IHtmlDocumentLoader
{
    private readonly HtmlWeb _web = new();

    public Task<HtmlDocument> LoadAsync(string url, CancellationToken cancellationToken)
    {
        return _web.LoadFromWebAsync(url, cancellationToken);
    }
}
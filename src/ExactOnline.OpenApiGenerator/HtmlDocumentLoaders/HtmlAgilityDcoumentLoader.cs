using HtmlAgilityPack;

namespace ExactOnline.OpenApiGenerator.HtmlDocumentLoaders;

internal class HtmlAgilityDcoumentLoader()
{
    private readonly HtmlWeb _web = new();

    public Task<HtmlDocument> LoadDocumentAsync(string url, CancellationToken cancellationToken)
    {
        return _web.LoadFromWebAsync(url, cancellationToken);
    }
}
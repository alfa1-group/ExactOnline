using HtmlAgilityPack;

namespace ExactOnline.OpenApiGenerator.HtmlDocumentLoaders;

internal interface IHtmlDocumentLoader
{
    Task<HtmlDocument> LoadAsync(string url, CancellationToken cancellationToken);
}
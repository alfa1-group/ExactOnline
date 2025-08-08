using HtmlAgilityPack;

namespace ExactOnline.OpenApiGenerator.HtmlDocumentLoaders;

internal interface IHtmlDocumentLoader : IDisposable
{
    Task<HtmlDocument> LoadAsync(string url, CancellationToken cancellationToken);
}
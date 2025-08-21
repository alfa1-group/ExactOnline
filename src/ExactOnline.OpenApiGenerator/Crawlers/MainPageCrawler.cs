using HtmlAgilityPack;

namespace ExactOnline.OpenApiGenerator.Crawlers;

internal static class MainPageCrawler
{
    /// <summary>
    /// Reads an HTML page from the given URL and extracts the "name" parameter
    /// from the href attribute of all "a" elements with the class "Endpoints".
    /// </summary>
    /// <param name="url">The URL of the HTML page to parse.</param>
    /// <returns>A string array containing the extracted names, or an empty array if none are found or an error occurs.</returns>
    internal static async Task<IReadOnlyList<string>> ExtractEndpointUrlsAsync(string url)
    {
        // Initialize a list to hold the extracted names.
        var urls = new List<string>();

        // Create a new HtmlWeb instance to load the HTML document from the URL.
        var web = new HtmlWeb();
        var document = await web.LoadFromWebAsync(url);

        // Select all <a> elements that have the class "Endpoints".
        // The XPath expression "//a[@class='Endpoints']" finds all <a> tags
        // anywhere in the document that have a 'class' attribute exactly equal to 'Endpoints'.
        var endpointNodes = document.DocumentNode.SelectNodes("//a[@class='Endpoints']");

        // Check if any nodes were found.
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (endpointNodes != null)
        {
            // Iterate through each found <a> node.
            foreach (var linkNode in endpointNodes)
            {
                // Get the value of the 'href' attribute for the current link.
                var href = linkNode.GetAttributeValue("href", string.Empty);

                // Ensure the href is not empty
                if (!string.IsNullOrEmpty(href))
                {
                    urls.Add("https://start.exactonline.nl/docs/" + href);
                }
            }
        }

        return urls
            .OrderBy(u => u)
            .ToArray();
    }
}
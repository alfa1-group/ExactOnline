using System.Globalization;
using System.Text.RegularExpressions;

namespace ExactOnline.OpenApiGenerator.Extensions;

internal static class StringExtensions
{
    private static readonly Regex WordsRegex = new("[^A-Za-z0-9]+", RegexOptions.Compiled);

    public static string ToPascalCase(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        var words = WordsRegex.Split(text).Where(s => !string.IsNullOrEmpty(s));
        return string.Concat(words.Select(word => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(word.ToLowerInvariant())));
    }
}
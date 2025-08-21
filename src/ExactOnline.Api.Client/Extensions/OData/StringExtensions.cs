// ReSharper disable once CheckNamespace
namespace ExactOnline.Api.Client.Extensions;

public static class StringExtensions
{
    public static string ToODataFormat(this string value)
    {
        return $"'{value}'";
    }
}
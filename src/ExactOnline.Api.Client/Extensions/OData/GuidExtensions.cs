// ReSharper disable once CheckNamespace
namespace ExactOnline.Api.Client.Extensions;

public static class GuidExtensions
{
    public static string ToODataFormat(this Guid value)
    {
        return $"guid'{value}'";
    }
}
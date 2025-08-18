namespace ExactOnline.Api.Client.Models;
public partial class LogisticsIncoterms
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Code), "Code" },
        { nameof(Description), "Description" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Version), "Version" }
    };
}

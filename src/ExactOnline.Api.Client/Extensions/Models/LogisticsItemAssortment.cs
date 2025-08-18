namespace ExactOnline.Api.Client.Models;
public partial class LogisticsItemAssortment
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Code), "Code" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Properties), "Properties" }
    };
}

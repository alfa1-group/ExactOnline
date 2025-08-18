namespace ExactOnline.Api.Client.Models;
public partial class ReadLogisticsItemExtraField
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Description), "Description" },
        { nameof(ItemID), "ItemID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Number), "Number" },
        { nameof(Value), "Value" }
    };
}

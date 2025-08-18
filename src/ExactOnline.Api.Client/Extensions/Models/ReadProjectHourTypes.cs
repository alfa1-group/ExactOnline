namespace ExactOnline.Api.Client.Models;
public partial class ReadProjectHourTypes
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(ItemCode), "ItemCode" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(ItemId), "ItemId" },
        { nameof(Metadata), "__metadata" }
    };
}

namespace ExactOnline.Api.Client.Models;
public partial class SyncInventorySerialBatchNumbers
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Division), "Division" },
        { nameof(EndDate), "EndDate" },
        { nameof(ID), "ID" },
        { nameof(IsBlocked), "IsBlocked" },
        { nameof(Item), "Item" },
        { nameof(ItemCode), "ItemCode" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Remarks), "Remarks" },
        { nameof(SerialBatchNumber), "SerialBatchNumber" },
        { nameof(StartDate), "StartDate" },
        { nameof(Timestamp), "Timestamp" },
        { nameof(Type), "Type" }
    };
}

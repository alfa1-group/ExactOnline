namespace ExactOnline.Api.Client.Models;
public partial class SyncInventoryItemStorageLocations
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Division), "Division" },
        { nameof(ID), "ID" },
        { nameof(Item), "Item" },
        { nameof(ItemCode), "ItemCode" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(MaximumStock), "MaximumStock" },
        { nameof(Metadata), "__metadata" },
        { nameof(MinimumStock), "MinimumStock" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(StorageLocation), "StorageLocation" },
        { nameof(StorageLocationCode), "StorageLocationCode" },
        { nameof(StorageLocationDescription), "StorageLocationDescription" },
        { nameof(Timestamp), "Timestamp" },
        { nameof(Type), "Type" },
        { nameof(Warehouse), "Warehouse" },
        { nameof(WarehouseCode), "WarehouseCode" },
        { nameof(WarehouseDescription), "WarehouseDescription" }
    };
}

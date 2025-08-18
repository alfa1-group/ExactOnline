namespace ExactOnline.Api.Client.Models;
public partial class SyncInventoryItemWarehouses
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(CountingCycle), "CountingCycle" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(DefaultStorageLocation), "DefaultStorageLocation" },
        { nameof(DefaultStorageLocationCode), "DefaultStorageLocationCode" },
        { nameof(DefaultStorageLocationDescription), "DefaultStorageLocationDescription" },
        { nameof(Division), "Division" },
        { nameof(ID), "ID" },
        { nameof(Item), "Item" },
        { nameof(ItemCode), "ItemCode" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(MaximumStock), "MaximumStock" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(OrderPolicy), "OrderPolicy" },
        { nameof(Period), "Period" },
        { nameof(ReorderPoint), "ReorderPoint" },
        { nameof(ReorderQuantity), "ReorderQuantity" },
        { nameof(ReplenishmentType), "ReplenishmentType" },
        { nameof(ReservedStock), "ReservedStock" },
        { nameof(SafetyStock), "SafetyStock" },
        { nameof(StorageLocationSequenceNumber), "StorageLocationSequenceNumber" },
        { nameof(Timestamp), "Timestamp" },
        { nameof(Warehouse), "Warehouse" },
        { nameof(WarehouseCode), "WarehouseCode" },
        { nameof(WarehouseDescription), "WarehouseDescription" }
    };
}

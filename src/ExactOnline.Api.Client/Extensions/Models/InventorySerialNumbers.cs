namespace ExactOnline.Api.Client.Models;
public partial class InventorySerialNumbers
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Available), "Available" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(CustomField), "CustomField" },
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
        { nameof(SerialNumber), "SerialNumber" },
        { nameof(StartDate), "StartDate" },
        { nameof(StorageLocation), "StorageLocation" },
        { nameof(StorageLocationCode), "StorageLocationCode" },
        { nameof(StorageLocationDescription), "StorageLocationDescription" },
        { nameof(Warehouse), "Warehouse" },
        { nameof(WarehouseCode), "WarehouseCode" },
        { nameof(WarehouseDescription), "WarehouseDescription" }
    };
}

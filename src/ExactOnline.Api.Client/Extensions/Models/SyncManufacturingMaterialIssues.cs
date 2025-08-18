namespace ExactOnline.Api.Client.Models;
public partial class SyncManufacturingMaterialIssues
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(CreatedBy), "CreatedBy" },
        { nameof(CreatedByFullName), "CreatedByFullName" },
        { nameof(CreatedDate), "CreatedDate" },
        { nameof(DraftStockTransactionID), "DraftStockTransactionID" },
        { nameof(HasReversibleQuantity), "HasReversibleQuantity" },
        { nameof(IsBackflush), "IsBackflush" },
        { nameof(IsBatch), "IsBatch" },
        { nameof(IsFractionAllowedItem), "IsFractionAllowedItem" },
        { nameof(IsIssueFromChild), "IsIssueFromChild" },
        { nameof(IsSerial), "IsSerial" },
        { nameof(Item), "Item" },
        { nameof(ItemCode), "ItemCode" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(ItemPictureUrl), "ItemPictureUrl" },
        { nameof(Metadata), "__metadata" },
        { nameof(Note), "Note" },
        { nameof(Quantity), "Quantity" },
        { nameof(RelatedStockTransaction), "RelatedStockTransaction" },
        { nameof(ShopOrder), "ShopOrder" },
        { nameof(ShopOrderMaterialPlan), "ShopOrderMaterialPlan" },
        { nameof(ShopOrderNumber), "ShopOrderNumber" },
        { nameof(StockTransactionId), "StockTransactionId" },
        { nameof(StorageLocation), "StorageLocation" },
        { nameof(StorageLocationCode), "StorageLocationCode" },
        { nameof(StorageLocationDescription), "StorageLocationDescription" },
        { nameof(Timestamp), "Timestamp" },
        { nameof(TransactionDate), "TransactionDate" },
        { nameof(Unit), "Unit" },
        { nameof(UnitDescription), "UnitDescription" },
        { nameof(Warehouse), "Warehouse" },
        { nameof(WarehouseCode), "WarehouseCode" },
        { nameof(WarehouseDescription), "WarehouseDescription" }
    };
}

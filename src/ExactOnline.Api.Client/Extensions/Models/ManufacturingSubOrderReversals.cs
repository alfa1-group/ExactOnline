namespace ExactOnline.Api.Client.Models;
public partial class ManufacturingSubOrderReversals
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(CreatedBy), "CreatedBy" },
        { nameof(CreatedByFullName), "CreatedByFullName" },
        { nameof(CreatedDate), "CreatedDate" },
        { nameof(IsBatch), "IsBatch" },
        { nameof(IsFractionAllowedItem), "IsFractionAllowedItem" },
        { nameof(IsSerial), "IsSerial" },
        { nameof(Item), "Item" },
        { nameof(ItemCode), "ItemCode" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(ItemPictureUrl), "ItemPictureUrl" },
        { nameof(MaterialReversalStockTransactionId), "MaterialReversalStockTransactionId" },
        { nameof(Metadata), "__metadata" },
        { nameof(Note), "Note" },
        { nameof(OriginalMaterialIssueStockTransactionId), "OriginalMaterialIssueStockTransactionId" },
        { nameof(OriginalShopOrderReceiptStockTransactionId), "OriginalShopOrderReceiptStockTransactionId" },
        { nameof(ParentShopOrder), "ParentShopOrder" },
        { nameof(ParentShopOrderNumber), "ParentShopOrderNumber" },
        { nameof(Quantity), "Quantity" },
        { nameof(ShopOrderReversalStockTransactionId), "ShopOrderReversalStockTransactionId" },
        { nameof(SubShopOrder), "SubShopOrder" },
        { nameof(SubShopOrderNumber), "SubShopOrderNumber" },
        { nameof(TransactionDate), "TransactionDate" },
        { nameof(Unit), "Unit" },
        { nameof(UnitDescription), "UnitDescription" },
        { nameof(Warehouse), "Warehouse" },
        { nameof(WarehouseCode), "WarehouseCode" },
        { nameof(WarehouseDescription), "WarehouseDescription" }
    };
}

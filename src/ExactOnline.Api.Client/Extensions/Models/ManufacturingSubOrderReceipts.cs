namespace ExactOnline.Api.Client.Models;
public partial class ManufacturingSubOrderReceipts
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(CreatedBy), "CreatedBy" },
        { nameof(CreatedByFullName), "CreatedByFullName" },
        { nameof(CreatedDate), "CreatedDate" },
        { nameof(DraftStockTransactionID), "DraftStockTransactionID" },
        { nameof(HasReversibleQuantity), "HasReversibleQuantity" },
        { nameof(IsBatch), "IsBatch" },
        { nameof(IsFractionAllowedItem), "IsFractionAllowedItem" },
        { nameof(IsSerial), "IsSerial" },
        { nameof(Item), "Item" },
        { nameof(ItemCode), "ItemCode" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(ItemPictureUrl), "ItemPictureUrl" },
        { nameof(MaterialIssueStockTransactionId), "MaterialIssueStockTransactionId" },
        { nameof(Metadata), "__metadata" },
        { nameof(ParentShopOrder), "ParentShopOrder" },
        { nameof(ParentShopOrderMaterialPlan), "ParentShopOrderMaterialPlan" },
        { nameof(ParentShopOrderNumber), "ParentShopOrderNumber" },
        { nameof(Quantity), "Quantity" },
        { nameof(ShopOrderReceiptStockTransactionId), "ShopOrderReceiptStockTransactionId" },
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

namespace ExactOnline.Api.Client.Models;
public partial class SyncManufacturingShopOrderPurchasePlanning
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Division), "Division" },
        { nameof(Factor), "Factor" },
        { nameof(FactorType), "FactorType" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(PurchaseOrder), "PurchaseOrder" },
        { nameof(PurchaseOrderNumber), "PurchaseOrderNumber" },
        { nameof(Quantity), "Quantity" },
        { nameof(ShopOrder), "ShopOrder" },
        { nameof(ShopOrderMaterialPlan), "ShopOrderMaterialPlan" },
        { nameof(ShopOrderNumber), "ShopOrderNumber" },
        { nameof(ShopOrderRoutingStepPlan), "ShopOrderRoutingStepPlan" },
        { nameof(Timestamp), "Timestamp" }
    };
}

namespace ExactOnline.Api.Client.Models;
public partial class SyncManufacturingShopOrderSubOrders
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Division), "Division" },
        { nameof(ID), "ID" },
        { nameof(Level), "Level" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(ShopOrder), "ShopOrder" },
        { nameof(ShopOrderMain), "ShopOrderMain" },
        { nameof(ShopOrderMainNumber), "ShopOrderMainNumber" },
        { nameof(ShopOrderMaterialPlan), "ShopOrderMaterialPlan" },
        { nameof(ShopOrderNumber), "ShopOrderNumber" },
        { nameof(ShopOrderParent), "ShopOrderParent" },
        { nameof(ShopOrderParentNumber), "ShopOrderParentNumber" },
        { nameof(Timestamp), "Timestamp" }
    };
}

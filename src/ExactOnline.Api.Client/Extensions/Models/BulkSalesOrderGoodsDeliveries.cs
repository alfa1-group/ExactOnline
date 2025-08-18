namespace ExactOnline.Api.Client.Models;
public partial class BulkSalesOrderGoodsDeliveries
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(DeliveryAccount), "DeliveryAccount" },
        { nameof(DeliveryAccountCode), "DeliveryAccountCode" },
        { nameof(DeliveryAccountName), "DeliveryAccountName" },
        { nameof(DeliveryAddress), "DeliveryAddress" },
        { nameof(DeliveryContact), "DeliveryContact" },
        { nameof(DeliveryContactPersonFullName), "DeliveryContactPersonFullName" },
        { nameof(DeliveryDate), "DeliveryDate" },
        { nameof(DeliveryNumber), "DeliveryNumber" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(Document), "Document" },
        { nameof(DocumentSubject), "DocumentSubject" },
        { nameof(EntryID), "EntryID" },
        { nameof(EntryNumber), "EntryNumber" },
        { nameof(GoodsDeliveryLines), "GoodsDeliveryLines" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Remarks), "Remarks" },
        { nameof(ShippingMethod), "ShippingMethod" },
        { nameof(ShippingMethodCode), "ShippingMethodCode" },
        { nameof(ShippingMethodDescription), "ShippingMethodDescription" },
        { nameof(TrackingNumber), "TrackingNumber" },
        { nameof(Warehouse), "Warehouse" },
        { nameof(WarehouseCode), "WarehouseCode" },
        { nameof(WarehouseDescription), "WarehouseDescription" }
    };
}

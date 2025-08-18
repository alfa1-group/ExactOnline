namespace ExactOnline.Api.Client.Models;
public partial class PurchaseOrderPurchaseReturns
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(Document), "Document" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(PurchaseReturnLines), "PurchaseReturnLines" },
        { nameof(Remarks), "Remarks" },
        { nameof(ReturnDate), "ReturnDate" },
        { nameof(ReturnNumber), "ReturnNumber" },
        { nameof(Status), "Status" },
        { nameof(Supplier), "Supplier" },
        { nameof(SupplierAddress), "SupplierAddress" },
        { nameof(SupplierContact), "SupplierContact" },
        { nameof(SupplierContactFullName), "SupplierContactFullName" },
        { nameof(TrackingNumber), "TrackingNumber" },
        { nameof(Warehouse), "Warehouse" },
        { nameof(WarehouseCode), "WarehouseCode" },
        { nameof(WarehouseDescription), "WarehouseDescription" },
        { nameof(YourRef), "YourRef" }
    };
}

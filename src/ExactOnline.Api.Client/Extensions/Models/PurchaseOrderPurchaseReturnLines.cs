namespace ExactOnline.Api.Client.Models;
public partial class PurchaseOrderPurchaseReturnLines
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(BatchNumbers), "BatchNumbers" },
        { nameof(CreateCredit), "CreateCredit" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Division), "Division" },
        { nameof(EntryID), "EntryID" },
        { nameof(Expense), "Expense" },
        { nameof(ExpenseDescription), "ExpenseDescription" },
        { nameof(GoodsReceiptLineID), "GoodsReceiptLineID" },
        { nameof(ID), "ID" },
        { nameof(Item), "Item" },
        { nameof(ItemCode), "ItemCode" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(LineNumber), "LineNumber" },
        { nameof(Location), "Location" },
        { nameof(LocationCode), "LocationCode" },
        { nameof(LocationDescription), "LocationDescription" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Notes), "Notes" },
        { nameof(Project), "Project" },
        { nameof(ProjectCode), "ProjectCode" },
        { nameof(ProjectDescription), "ProjectDescription" },
        { nameof(PurchaseOrderLineID), "PurchaseOrderLineID" },
        { nameof(PurchaseOrderNumber), "PurchaseOrderNumber" },
        { nameof(Rebill), "Rebill" },
        { nameof(ReceiptNumber), "ReceiptNumber" },
        { nameof(ReceivedQuantity), "ReceivedQuantity" },
        { nameof(ReturnQuantity), "ReturnQuantity" },
        { nameof(ReturnReasonCodeDescription), "ReturnReasonCodeDescription" },
        { nameof(ReturnReasonCodeID), "ReturnReasonCodeID" },
        { nameof(SerialNumbers), "SerialNumbers" },
        { nameof(SupplierItemCode), "SupplierItemCode" },
        { nameof(UnitCode), "UnitCode" }
    };
}

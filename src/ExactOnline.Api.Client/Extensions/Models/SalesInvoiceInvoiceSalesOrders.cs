namespace ExactOnline.Api.Client.Models;
public partial class SalesInvoiceInvoiceSalesOrders
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(CreateMode), "CreateMode" },
        { nameof(DeliveryNumber), "DeliveryNumber" },
        { nameof(EndDate), "EndDate" },
        { nameof(ID), "ID" },
        { nameof(InvoiceMode), "InvoiceMode" },
        { nameof(JournalCode), "JournalCode" },
        { nameof(Metadata), "__metadata" },
        { nameof(SalesOrderIDs), "SalesOrderIDs" },
        { nameof(StartDate), "StartDate" },
        { nameof(UserInvoiceDate), "UserInvoiceDate" }
    };
}

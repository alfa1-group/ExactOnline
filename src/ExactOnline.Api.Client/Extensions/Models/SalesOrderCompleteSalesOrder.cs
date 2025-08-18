namespace ExactOnline.Api.Client.Models;
public partial class SalesOrderCompleteSalesOrder
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(CompleteDelivery), "CompleteDelivery" },
        { nameof(CompleteInvoice), "CompleteInvoice" },
        { nameof(Division), "Division" },
        { nameof(ErrorMessage), "ErrorMessage" },
        { nameof(Metadata), "__metadata" },
        { nameof(OrderID), "OrderID" },
        { nameof(SuccessMessage), "SuccessMessage" }
    };
}

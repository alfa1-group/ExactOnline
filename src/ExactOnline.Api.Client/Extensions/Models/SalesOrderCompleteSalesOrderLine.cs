namespace ExactOnline.Api.Client.Models;
public partial class SalesOrderCompleteSalesOrderLine
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(CompleteDelivery), "CompleteDelivery" },
        { nameof(CompleteInvoice), "CompleteInvoice" },
        { nameof(Division), "Division" },
        { nameof(ErrorMessage), "ErrorMessage" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(SuccessMessage), "SuccessMessage" }
    };
}

namespace ExactOnline.Api.Client.Models;
public partial class CRMReopenQuotation
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Division), "Division" },
        { nameof(ErrorMessage), "ErrorMessage" },
        { nameof(Metadata), "__metadata" },
        { nameof(QuotationID), "QuotationID" },
        { nameof(SuccessMessage), "SuccessMessage" }
    };
}

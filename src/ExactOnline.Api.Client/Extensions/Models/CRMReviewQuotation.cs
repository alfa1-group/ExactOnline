namespace ExactOnline.Api.Client.Models;
public partial class CRMReviewQuotation
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(CopyItemPrices), "CopyItemPrices" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(Document), "Document" },
        { nameof(ErrorMessage), "ErrorMessage" },
        { nameof(Metadata), "__metadata" },
        { nameof(NewQuotationID), "NewQuotationID" },
        { nameof(OrderAccount), "OrderAccount" },
        { nameof(OrderAccountContact), "OrderAccountContact" },
        { nameof(PaymentCondition), "PaymentCondition" },
        { nameof(QuotationDate), "QuotationDate" },
        { nameof(QuotationID), "QuotationID" },
        { nameof(SuccessMessage), "SuccessMessage" }
    };
}

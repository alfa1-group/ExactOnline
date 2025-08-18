namespace ExactOnline.Api.Client.Models;
public partial class CRMPrintQuotation
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Division), "Division" },
        { nameof(Document), "Document" },
        { nameof(DocumentCreationError), "DocumentCreationError" },
        { nameof(DocumentCreationSuccess), "DocumentCreationSuccess" },
        { nameof(DocumentLayout), "DocumentLayout" },
        { nameof(EmailCreationError), "EmailCreationError" },
        { nameof(EmailLayout), "EmailLayout" },
        { nameof(ExtraText), "ExtraText" },
        { nameof(Metadata), "__metadata" },
        { nameof(QuotationDate), "QuotationDate" },
        { nameof(QuotationID), "QuotationID" },
        { nameof(SendEmailToCustomer), "SendEmailToCustomer" },
        { nameof(SenderEmailAddress), "SenderEmailAddress" }
    };
}

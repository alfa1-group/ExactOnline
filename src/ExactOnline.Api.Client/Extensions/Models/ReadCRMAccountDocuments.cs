namespace ExactOnline.Api.Client.Models;
public partial class ReadCRMAccountDocuments
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Account), "Account" },
        { nameof(Attachments), "Attachments" },
        { nameof(Contact), "Contact" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Division), "Division" },
        { nameof(DocumentDate), "DocumentDate" },
        { nameof(DocumentFolder), "DocumentFolder" },
        { nameof(DocumentViewUrl), "DocumentViewUrl" },
        { nameof(HID), "HID" },
        { nameof(HasEmptyBody), "HasEmptyBody" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(Opportunity), "Opportunity" },
        { nameof(PurchaseInvoiceNumber), "PurchaseInvoiceNumber" },
        { nameof(PurchaseOrderNumber), "PurchaseOrderNumber" },
        { nameof(SalesInvoiceNumber), "SalesInvoiceNumber" },
        { nameof(SalesOrderNumber), "SalesOrderNumber" },
        { nameof(SendMethod), "SendMethod" },
        { nameof(Share), "Share" },
        { nameof(SharePointConnectionStatus), "SharePointConnectionStatus" },
        { nameof(SharePointID), "SharePointID" },
        { nameof(Source), "Source" },
        { nameof(SourceDescription), "SourceDescription" },
        { nameof(Subject), "Subject" },
        { nameof(Type), "Type" },
        { nameof(TypeDescription), "TypeDescription" }
    };
}

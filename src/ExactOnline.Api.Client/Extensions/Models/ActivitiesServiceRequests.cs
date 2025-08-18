namespace ExactOnline.Api.Client.Models;
public partial class ActivitiesServiceRequests
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Account), "Account" },
        { nameof(AccountName), "AccountName" },
        { nameof(AssignedTo), "AssignedTo" },
        { nameof(AssignedToFullName), "AssignedToFullName" },
        { nameof(Attachments), "Attachments" },
        { nameof(Contact), "Contact" },
        { nameof(ContactFullName), "ContactFullName" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(Document), "Document" },
        { nameof(DocumentSubject), "DocumentSubject" },
        { nameof(HID), "HID" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(NextAction), "NextAction" },
        { nameof(Notes), "Notes" },
        { nameof(ReceiptDate), "ReceiptDate" },
        { nameof(Status), "Status" },
        { nameof(StatusDescription), "StatusDescription" }
    };
}

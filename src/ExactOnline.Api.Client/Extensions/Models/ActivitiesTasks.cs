namespace ExactOnline.Api.Client.Models;
public partial class ActivitiesTasks
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Account), "Account" },
        { nameof(AccountName), "AccountName" },
        { nameof(ActionDate), "ActionDate" },
        { nameof(Attachments), "Attachments" },
        { nameof(Contact), "Contact" },
        { nameof(ContactFullName), "ContactFullName" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(CustomTaskType), "CustomTaskType" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(Document), "Document" },
        { nameof(DocumentSubject), "DocumentSubject" },
        { nameof(Employee), "Employee" },
        { nameof(HID), "HID" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Notes), "Notes" },
        { nameof(Opportunity), "Opportunity" },
        { nameof(OpportunityName), "OpportunityName" },
        { nameof(Project), "Project" },
        { nameof(ProjectDescription), "ProjectDescription" },
        { nameof(Status), "Status" },
        { nameof(StatusDescription), "StatusDescription" },
        { nameof(TaskType), "TaskType" },
        { nameof(TaskTypeDescription), "TaskTypeDescription" },
        { nameof(User), "User" },
        { nameof(UserFullName), "UserFullName" }
    };
}

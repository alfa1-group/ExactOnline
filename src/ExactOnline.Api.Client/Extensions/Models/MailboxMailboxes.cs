namespace ExactOnline.Api.Client.Models;
public partial class MailboxMailboxes
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Account), "Account" },
        { nameof(AccountName), "AccountName" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Description), "Description" },
        { nameof(ForDivision), "ForDivision" },
        { nameof(ForDivisionDescription), "ForDivisionDescription" },
        { nameof(ID), "ID" },
        { nameof(Mailbox), "Mailbox" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Publish), "Publish" },
        { nameof(Type), "Type" },
        { nameof(ValidFrom), "ValidFrom" },
        { nameof(ValidTo), "ValidTo" }
    };
}

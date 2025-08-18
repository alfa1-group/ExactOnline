namespace ExactOnline.Api.Client.Models;
public partial class MailboxMailMessagesSent
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Bank), "Bank" },
        { nameof(BankAccount), "BankAccount" },
        { nameof(Country), "Country" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(ForDivision), "ForDivision" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Operation), "Operation" },
        { nameof(OriginalMessage), "OriginalMessage" },
        { nameof(OriginalMessageSubject), "OriginalMessageSubject" },
        { nameof(PartnerKey), "PartnerKey" },
        { nameof(Quantity), "Quantity" },
        { nameof(RecipientAccount), "RecipientAccount" },
        { nameof(RecipientDeleted), "RecipientDeleted" },
        { nameof(RecipientMailbox), "RecipientMailbox" },
        { nameof(RecipientMailboxDescription), "RecipientMailboxDescription" },
        { nameof(RecipientMailboxID), "RecipientMailboxID" },
        { nameof(RecipientStatus), "RecipientStatus" },
        { nameof(RecipientStatusDescription), "RecipientStatusDescription" },
        { nameof(SenderAccount), "SenderAccount" },
        { nameof(SenderDateSent), "SenderDateSent" },
        { nameof(SenderDeleted), "SenderDeleted" },
        { nameof(SenderIPAddress), "SenderIPAddress" },
        { nameof(SenderMailbox), "SenderMailbox" },
        { nameof(SenderMailboxDescription), "SenderMailboxDescription" },
        { nameof(SenderMailboxID), "SenderMailboxID" },
        { nameof(SkipRecipientMailBoxAddressOverride), "SkipRecipientMailBoxAddressOverride" },
        { nameof(Subject), "Subject" },
        { nameof(SynchronizationCode), "SynchronizationCode" },
        { nameof(Type), "Type" }
    };
}

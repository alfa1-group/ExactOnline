namespace ExactOnline.Api.Client.Models;
public partial class ReadProjectCostsById
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(AccountCode), "AccountCode" },
        { nameof(AccountId), "AccountId" },
        { nameof(AccountName), "AccountName" },
        { nameof(AmountApproved), "AmountApproved" },
        { nameof(AmountDraft), "AmountDraft" },
        { nameof(AmountRejected), "AmountRejected" },
        { nameof(AmountSubmitted), "AmountSubmitted" },
        { nameof(CurrencyCode), "CurrencyCode" },
        { nameof(Date), "Date" },
        { nameof(EntryId), "EntryId" },
        { nameof(Expense), "Expense" },
        { nameof(ExpenseDescription), "ExpenseDescription" },
        { nameof(Id), "Id" },
        { nameof(ItemCode), "ItemCode" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(ItemId), "ItemId" },
        { nameof(Metadata), "__metadata" },
        { nameof(Notes), "Notes" },
        { nameof(ProjectCode), "ProjectCode" },
        { nameof(ProjectDescription), "ProjectDescription" },
        { nameof(ProjectId), "ProjectId" },
        { nameof(QuantityApproved), "QuantityApproved" },
        { nameof(QuantityDraft), "QuantityDraft" },
        { nameof(QuantityRejected), "QuantityRejected" },
        { nameof(QuantitySubmitted), "QuantitySubmitted" },
        { nameof(WeekNumber), "WeekNumber" }
    };
}

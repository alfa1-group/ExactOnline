namespace ExactOnline.Api.Client.Models;
public partial class ReadProjectCostEntryRecentAccounts
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(AccountId), "AccountId" },
        { nameof(AccountName), "AccountName" },
        { nameof(DateLastUsed), "DateLastUsed" },
        { nameof(Metadata), "__metadata" }
    };
}

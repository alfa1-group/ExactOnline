namespace ExactOnline.Api.Client.Models;
public partial class FinancialTransactionCashEntries
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(CashEntryLines), "CashEntryLines" },
        { nameof(ClosingBalanceFC), "ClosingBalanceFC" },
        { nameof(Created), "Created" },
        { nameof(Currency), "Currency" },
        { nameof(CustomField), "CustomField" },
        { nameof(Division), "Division" },
        { nameof(EntryID), "EntryID" },
        { nameof(EntryNumber), "EntryNumber" },
        { nameof(FinancialPeriod), "FinancialPeriod" },
        { nameof(FinancialYear), "FinancialYear" },
        { nameof(JournalCode), "JournalCode" },
        { nameof(JournalDescription), "JournalDescription" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(OpeningBalanceFC), "OpeningBalanceFC" },
        { nameof(Status), "Status" },
        { nameof(StatusDescription), "StatusDescription" }
    };
}

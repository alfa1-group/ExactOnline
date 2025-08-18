namespace ExactOnline.Api.Client.Models;
public partial class CashflowBanks
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(BICCode), "BICCode" },
        { nameof(BankName), "BankName" },
        { nameof(Country), "Country" },
        { nameof(Created), "Created" },
        { nameof(Description), "Description" },
        { nameof(Format), "Format" },
        { nameof(HomePageAddress), "HomePageAddress" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Status), "Status" }
    };
}

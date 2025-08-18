namespace ExactOnline.Api.Client.Models;
public partial class ReadFinancialAgingOverviewByAccount
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(AgeGroup), "AgeGroup" },
        { nameof(AgeGroupDescription), "AgeGroupDescription" },
        { nameof(AmountPayable), "AmountPayable" },
        { nameof(AmountReceivable), "AmountReceivable" },
        { nameof(CurrencyCode), "CurrencyCode" },
        { nameof(Metadata), "__metadata" }
    };
}

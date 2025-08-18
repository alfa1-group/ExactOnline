namespace ExactOnline.Api.Client.Models;
public partial class ReadFinancialRevenueListByYear
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Amount), "Amount" },
        { nameof(Metadata), "__metadata" },
        { nameof(Period), "Period" },
        { nameof(Year), "Year" }
    };
}

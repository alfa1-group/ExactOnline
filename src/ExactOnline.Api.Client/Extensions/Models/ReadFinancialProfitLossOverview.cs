namespace ExactOnline.Api.Client.Models;
public partial class ReadFinancialProfitLossOverview
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(CostsCurrentPeriod), "CostsCurrentPeriod" },
        { nameof(CostsCurrentYear), "CostsCurrentYear" },
        { nameof(CostsPreviousYear), "CostsPreviousYear" },
        { nameof(CostsPreviousYearPeriod), "CostsPreviousYearPeriod" },
        { nameof(CurrencyCode), "CurrencyCode" },
        { nameof(CurrentPeriod), "CurrentPeriod" },
        { nameof(CurrentYear), "CurrentYear" },
        { nameof(Metadata), "__metadata" },
        { nameof(PreviousYear), "PreviousYear" },
        { nameof(PreviousYearPeriod), "PreviousYearPeriod" },
        { nameof(ResultCurrentPeriod), "ResultCurrentPeriod" },
        { nameof(ResultCurrentYear), "ResultCurrentYear" },
        { nameof(ResultPreviousYear), "ResultPreviousYear" },
        { nameof(ResultPreviousYearPeriod), "ResultPreviousYearPeriod" },
        { nameof(RevenueCurrentPeriod), "RevenueCurrentPeriod" },
        { nameof(RevenueCurrentYear), "RevenueCurrentYear" },
        { nameof(RevenuePreviousYear), "RevenuePreviousYear" },
        { nameof(RevenuePreviousYearPeriod), "RevenuePreviousYearPeriod" }
    };
}

namespace ExactOnline.Api.Client.Models;
public partial class FinancialReportingBalance
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Amount), "Amount" },
        { nameof(AmountCredit), "AmountCredit" },
        { nameof(AmountDebit), "AmountDebit" },
        { nameof(BalanceType), "BalanceType" },
        { nameof(CostCenterCode), "CostCenterCode" },
        { nameof(CostCenterDescription), "CostCenterDescription" },
        { nameof(CostUnitCode), "CostUnitCode" },
        { nameof(CostUnitDescription), "CostUnitDescription" },
        { nameof(Count), "Count" },
        { nameof(Division), "Division" },
        { nameof(GLAccount), "GLAccount" },
        { nameof(GLAccountCode), "GLAccountCode" },
        { nameof(GLAccountDescription), "GLAccountDescription" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(ReportingPeriod), "ReportingPeriod" },
        { nameof(ReportingYear), "ReportingYear" },
        { nameof(Status), "Status" },
        { nameof(Type), "Type" }
    };
}

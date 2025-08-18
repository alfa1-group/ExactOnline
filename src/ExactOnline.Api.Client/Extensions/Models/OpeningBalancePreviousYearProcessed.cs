namespace ExactOnline.Api.Client.Models;
public partial class OpeningBalancePreviousYearProcessed
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Amount), "Amount" },
        { nameof(BalanceSide), "BalanceSide" },
        { nameof(Division), "Division" },
        { nameof(GLAccount), "GLAccount" },
        { nameof(GLAccountCode), "GLAccountCode" },
        { nameof(GLAccountDescription), "GLAccountDescription" },
        { nameof(Metadata), "__metadata" },
        { nameof(ReportingYear), "ReportingYear" }
    };
}

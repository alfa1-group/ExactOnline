namespace ExactOnline.Api.Client.Models;
public partial class FinancialProcessReturn
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Metadata), "__metadata" },
        { nameof(Processed), "Processed" },
        { nameof(Request), "Request" },
        { nameof(Status), "Status" }
    };
}

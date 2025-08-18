namespace ExactOnline.Api.Client.Models;
public partial class FinancialGLTransactionTypes
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Description), "Description" },
        { nameof(DescriptionSuffix), "DescriptionSuffix" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" }
    };
}

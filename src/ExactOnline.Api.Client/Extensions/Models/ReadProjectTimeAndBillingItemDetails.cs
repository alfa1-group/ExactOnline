namespace ExactOnline.Api.Client.Models;
public partial class ReadProjectTimeAndBillingItemDetails
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Code), "Code" },
        { nameof(Description), "Description" },
        { nameof(ID), "ID" },
        { nameof(IsFractionAllowedItem), "IsFractionAllowedItem" },
        { nameof(IsSalesItem), "IsSalesItem" },
        { nameof(Metadata), "__metadata" },
        { nameof(SalesCurrency), "SalesCurrency" },
        { nameof(SalesPrice), "SalesPrice" }
    };
}

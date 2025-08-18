namespace ExactOnline.Api.Client.Models;
public partial class GeneralCurrencies
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(AmountPrecision), "AmountPrecision" },
        { nameof(Code), "Code" },
        { nameof(Created), "Created" },
        { nameof(Description), "Description" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(PricePrecision), "PricePrecision" }
    };
}

namespace ExactOnline.Api.Client.Models;
public partial class InventoryProcessStockCount
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Division), "Division" },
        { nameof(ErrorMessage), "ErrorMessage" },
        { nameof(Metadata), "__metadata" },
        { nameof(StockCountID), "StockCountID" },
        { nameof(SuccessMessage), "SuccessMessage" }
    };
}

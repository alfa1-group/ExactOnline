namespace ExactOnline.Api.Client.Models;
public partial class LogisticsItemChargeRelation
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Amount), "Amount" },
        { nameof(ChargeCode), "ChargeCode" },
        { nameof(ChargeDescription), "ChargeDescription" },
        { nameof(ChargeID), "ChargeID" },
        { nameof(ChargeVATCode), "ChargeVATCode" },
        { nameof(ChargeVATDescription), "ChargeVATDescription" },
        { nameof(ChargeVATPercentage), "ChargeVATPercentage" },
        { nameof(ChargeVATType), "ChargeVATType" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Currency), "Currency" },
        { nameof(Division), "Division" },
        { nameof(ID), "ID" },
        { nameof(ItemCode), "ItemCode" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(ItemID), "ItemID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Quantity), "Quantity" },
        { nameof(TotalAmount), "TotalAmount" }
    };
}

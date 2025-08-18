namespace ExactOnline.Api.Client.Models;
public partial class LogisticsItemVersions
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(BatchQuantity), "BatchQuantity" },
        { nameof(CalculatedCostPrice), "CalculatedCostPrice" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(ID), "ID" },
        { nameof(IsDefault), "IsDefault" },
        { nameof(Item), "Item" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(LeadTime), "LeadTime" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Notes), "Notes" },
        { nameof(Status), "Status" },
        { nameof(StatusDescription), "StatusDescription" },
        { nameof(Type), "Type" },
        { nameof(TypeDescription), "TypeDescription" },
        { nameof(VersionDate), "VersionDate" },
        { nameof(VersionNumber), "VersionNumber" }
    };
}

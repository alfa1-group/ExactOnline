namespace ExactOnline.Api.Client.Models;
public partial class ManufacturingProductionAreas
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Code), "Code" },
        { nameof(Costcenter), "Costcenter" },
        { nameof(CostcenterDescription), "CostcenterDescription" },
        { nameof(Costunit), "Costunit" },
        { nameof(CostunitDescription), "CostunitDescription" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(ID), "ID" },
        { nameof(IsDefault), "IsDefault" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Notes), "Notes" }
    };
}

namespace ExactOnline.Api.Client.Models;
public partial class InventoryWarehouses
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Code), "Code" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(DefaultStorageLocation), "DefaultStorageLocation" },
        { nameof(DefaultStorageLocationCode), "DefaultStorageLocationCode" },
        { nameof(DefaultStorageLocationDescription), "DefaultStorageLocationDescription" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(EMail), "EMail" },
        { nameof(ID), "ID" },
        { nameof(Main), "Main" },
        { nameof(ManagerUser), "ManagerUser" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(UseStorageLocations), "UseStorageLocations" }
    };
}

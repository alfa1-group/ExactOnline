namespace ExactOnline.Api.Client.Models;
public partial class ManufacturingOperations
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Code), "Code" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(HasSuppliers), "HasSuppliers" },
        { nameof(ID), "ID" },
        { nameof(Item), "Item" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Notes), "Notes" },
        { nameof(Searchcode), "Searchcode" },
        { nameof(Status), "Status" }
    };
}

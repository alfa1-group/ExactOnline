namespace ExactOnline.Api.Client.Models;
public partial class CRMAccountClassifications
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(AccountClassificationName), "AccountClassificationName" },
        { nameof(AccountClassificationNameDescription), "AccountClassificationNameDescription" },
        { nameof(Code), "Code" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" }
    };
}

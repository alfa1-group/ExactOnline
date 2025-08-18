namespace ExactOnline.Api.Client.Models;
public partial class SalesOrderCharges
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Active), "Active" },
        { nameof(Amount), "Amount" },
        { nameof(Code), "Code" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(GLAccount), "GLAccount" },
        { nameof(GLAccountCode), "GLAccountCode" },
        { nameof(GLAccountDescription), "GLAccountDescription" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(VATCode), "VATCode" },
        { nameof(VATDescription), "VATDescription" },
        { nameof(VATPercentage), "VATPercentage" }
    };
}

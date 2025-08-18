namespace ExactOnline.Api.Client.Models;
public partial class UsersUserRoles
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Description), "Description" },
        { nameof(EndDate), "EndDate" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Role), "Role" },
        { nameof(RoleLevel), "RoleLevel" },
        { nameof(StartDate), "StartDate" },
        { nameof(UserID), "UserID" }
    };
}

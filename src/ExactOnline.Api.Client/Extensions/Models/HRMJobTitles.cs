namespace ExactOnline.Api.Client.Models;
public partial class HRMJobTitles
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Code), "Code" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(ID), "ID" },
        { nameof(JobCode), "JobCode" },
        { nameof(JobGroup), "JobGroup" },
        { nameof(JobGroupCode), "JobGroupCode" },
        { nameof(JobGroupDescription), "JobGroupDescription" },
        { nameof(JobLevelFrom), "JobLevelFrom" },
        { nameof(JobLevelTo), "JobLevelTo" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Notes), "Notes" }
    };
}

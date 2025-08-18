namespace ExactOnline.Api.Client.Models;
public partial class PayrollEmployments
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Division), "Division" },
        { nameof(Employee), "Employee" },
        { nameof(EmployeeFullName), "EmployeeFullName" },
        { nameof(EmployeeHID), "EmployeeHID" },
        { nameof(EmploymentNumber), "EmploymentNumber" },
        { nameof(EndDate), "EndDate" },
        { nameof(HID), "HID" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(ReasonEnd), "ReasonEnd" },
        { nameof(ReasonEndDescription), "ReasonEndDescription" },
        { nameof(ReasonEndFlex), "ReasonEndFlex" },
        { nameof(ReasonEndFlexDescription), "ReasonEndFlexDescription" },
        { nameof(StartDate), "StartDate" },
        { nameof(StartDateOrganization), "StartDateOrganization" }
    };
}

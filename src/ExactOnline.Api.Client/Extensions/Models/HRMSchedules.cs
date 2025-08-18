namespace ExactOnline.Api.Client.Models;
public partial class HRMSchedules
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Active), "Active" },
        { nameof(AverageHours), "AverageHours" },
        { nameof(BillabilityTarget), "BillabilityTarget" },
        { nameof(Code), "Code" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Days), "Days" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(Employee), "Employee" },
        { nameof(EmployeeFullName), "EmployeeFullName" },
        { nameof(Employment), "Employment" },
        { nameof(EmploymentCLA), "EmploymentCLA" },
        { nameof(EmploymentHID), "EmploymentHID" },
        { nameof(EmploymentNumber), "EmploymentNumber" },
        { nameof(EndDate), "EndDate" },
        { nameof(Hours), "Hours" },
        { nameof(ID), "ID" },
        { nameof(LeaveHoursCompensation), "LeaveHoursCompensation" },
        { nameof(Main), "Main" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(PaymentParttimeFactor), "PaymentParttimeFactor" },
        { nameof(ScheduleEntries), "ScheduleEntries" },
        { nameof(ScheduleType), "ScheduleType" },
        { nameof(ScheduleTypeDescription), "ScheduleTypeDescription" },
        { nameof(StartDate), "StartDate" },
        { nameof(StartWeek), "StartWeek" }
    };
}

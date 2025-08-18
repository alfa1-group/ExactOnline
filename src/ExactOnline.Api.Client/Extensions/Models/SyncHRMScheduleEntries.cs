namespace ExactOnline.Api.Client.Models;
public partial class SyncHRMScheduleEntries
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Break), "Break" },
        { nameof(BreakEndTime), "BreakEndTime" },
        { nameof(BreakStartTime), "BreakStartTime" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Day), "Day" },
        { nameof(DayIsSelected), "DayIsSelected" },
        { nameof(Division), "Division" },
        { nameof(Employee), "Employee" },
        { nameof(EmployeeFullName), "EmployeeFullName" },
        { nameof(Employment), "Employment" },
        { nameof(EmploymentNumber), "EmploymentNumber" },
        { nameof(EndTime), "EndTime" },
        { nameof(Hours), "Hours" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Schedule), "Schedule" },
        { nameof(ScheduleActivityType), "ScheduleActivityType" },
        { nameof(ScheduleType), "ScheduleType" },
        { nameof(StartTime), "StartTime" },
        { nameof(Timestamp), "Timestamp" },
        { nameof(WeekNumber), "WeekNumber" }
    };
}

namespace ExactOnline.Api.Client.Models;
public partial class SyncHRMLeaveRegistrations
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(Employee), "Employee" },
        { nameof(EmployeeFullName), "EmployeeFullName" },
        { nameof(EmployeeHID), "EmployeeHID" },
        { nameof(EndDate), "EndDate" },
        { nameof(EndTime), "EndTime" },
        { nameof(Hours), "Hours" },
        { nameof(HoursFirstDay), "HoursFirstDay" },
        { nameof(HoursLastDay), "HoursLastDay" },
        { nameof(ID), "ID" },
        { nameof(LeaveType), "LeaveType" },
        { nameof(LeaveTypeCode), "LeaveTypeCode" },
        { nameof(LeaveTypeDescription), "LeaveTypeDescription" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Notes), "Notes" },
        { nameof(StartDate), "StartDate" },
        { nameof(StartTime), "StartTime" },
        { nameof(Status), "Status" },
        { nameof(Timestamp), "Timestamp" }
    };
}

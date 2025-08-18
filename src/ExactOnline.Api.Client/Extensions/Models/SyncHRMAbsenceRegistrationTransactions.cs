namespace ExactOnline.Api.Client.Models;
public partial class SyncHRMAbsenceRegistrationTransactions
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(AbsenceRegistration), "AbsenceRegistration" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Division), "Division" },
        { nameof(EndTime), "EndTime" },
        { nameof(ExpectedEndDate), "ExpectedEndDate" },
        { nameof(Hours), "Hours" },
        { nameof(HoursFirstDay), "HoursFirstDay" },
        { nameof(HoursLastDay), "HoursLastDay" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Notes), "Notes" },
        { nameof(NotificationMoment), "NotificationMoment" },
        { nameof(PercentageDisablement), "PercentageDisablement" },
        { nameof(StartDate), "StartDate" },
        { nameof(StartTime), "StartTime" },
        { nameof(Status), "Status" },
        { nameof(Timestamp), "Timestamp" }
    };
}

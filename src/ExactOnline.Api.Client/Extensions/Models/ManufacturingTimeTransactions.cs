namespace ExactOnline.Api.Client.Models;
public partial class ManufacturingTimeTransactions
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Activity), "Activity" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Date), "Date" },
        { nameof(Division), "Division" },
        { nameof(Employee), "Employee" },
        { nameof(Hours), "Hours" },
        { nameof(ID), "ID" },
        { nameof(IsOperationFinished), "IsOperationFinished" },
        { nameof(LaborHours), "LaborHours" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Notes), "Notes" },
        { nameof(PercentComplete), "PercentComplete" },
        { nameof(Quantity), "Quantity" },
        { nameof(RoutingStepPlan), "RoutingStepPlan" },
        { nameof(ShopOrder), "ShopOrder" },
        { nameof(Status), "Status" },
        { nameof(TimedTimeTransaction), "TimedTimeTransaction" },
        { nameof(WorkCenter), "WorkCenter" }
    };
}

namespace ExactOnline.Api.Client.Models;
public partial class InventoryItemWarehousePlanningDetails
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(ID), "ID" },
        { nameof(Item), "Item" },
        { nameof(ItemCode), "ItemCode" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(Metadata), "__metadata" },
        { nameof(PlannedDate), "PlannedDate" },
        { nameof(PlannedQuantity), "PlannedQuantity" },
        { nameof(PlanningSourceDescription), "PlanningSourceDescription" },
        { nameof(PlanningSourceID), "PlanningSourceID" },
        { nameof(PlanningSourceLineNumber), "PlanningSourceLineNumber" },
        { nameof(PlanningSourceNumber), "PlanningSourceNumber" },
        { nameof(PlanningSourceUrl), "PlanningSourceUrl" },
        { nameof(PlanningType), "PlanningType" },
        { nameof(PlanningTypeDescription), "PlanningTypeDescription" },
        { nameof(Warehouse), "Warehouse" },
        { nameof(WarehouseCode), "WarehouseCode" },
        { nameof(WarehouseDescription), "WarehouseDescription" }
    };
}

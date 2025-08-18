namespace ExactOnline.Api.Client.Models;
public partial class SyncInventoryStockPositions
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(CurrentStock), "CurrentStock" },
        { nameof(Division), "Division" },
        { nameof(FreeStock), "FreeStock" },
        { nameof(ID), "ID" },
        { nameof(ItemCode), "ItemCode" },
        { nameof(ItemDescription), "ItemDescription" },
        { nameof(ItemId), "ItemId" },
        { nameof(Metadata), "__metadata" },
        { nameof(PlanningIn), "PlanningIn" },
        { nameof(PlanningOut), "PlanningOut" },
        { nameof(ProjectedStock), "ProjectedStock" },
        { nameof(ReorderPoint), "ReorderPoint" },
        { nameof(ReservedStock), "ReservedStock" },
        { nameof(Timestamp), "Timestamp" },
        { nameof(UnitCode), "UnitCode" },
        { nameof(UnitDescription), "UnitDescription" },
        { nameof(Warehouse), "Warehouse" },
        { nameof(WarehouseCode), "WarehouseCode" },
        { nameof(WarehouseDescription), "WarehouseDescription" }
    };
}

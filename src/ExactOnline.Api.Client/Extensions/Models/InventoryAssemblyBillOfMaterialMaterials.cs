namespace ExactOnline.Api.Client.Models;
public partial class InventoryAssemblyBillOfMaterialMaterials
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(AssembledItem), "AssembledItem" },
        { nameof(AssembledItemCode), "AssembledItemCode" },
        { nameof(AssembledItemDescription), "AssembledItemDescription" },
        { nameof(AssembledLeadDays), "AssembledLeadDays" },
        { nameof(BatchQuantity), "BatchQuantity" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(Division), "Division" },
        { nameof(ID), "ID" },
        { nameof(LineNumber), "LineNumber" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(PartItem), "PartItem" },
        { nameof(PartItemCode), "PartItemCode" },
        { nameof(PartItemDescription), "PartItemDescription" },
        { nameof(Quantity), "Quantity" },
        { nameof(QuantityBatch), "QuantityBatch" },
        { nameof(UpdateCostPrice), "UpdateCostPrice" },
        { nameof(UseExplosion), "UseExplosion" }
    };
}

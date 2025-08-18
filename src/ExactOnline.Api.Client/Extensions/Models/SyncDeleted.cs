namespace ExactOnline.Api.Client.Models;
public partial class SyncDeleted
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(DeletedBy), "DeletedBy" },
        { nameof(DeletedDate), "DeletedDate" },
        { nameof(Division), "Division" },
        { nameof(EntityKey), "EntityKey" },
        { nameof(EntityType), "EntityType" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Timestamp), "Timestamp" }
    };
}

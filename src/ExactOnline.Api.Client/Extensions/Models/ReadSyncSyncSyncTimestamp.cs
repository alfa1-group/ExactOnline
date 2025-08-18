namespace ExactOnline.Api.Client.Models;
public partial class ReadSyncSyncSyncTimestamp
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(API), "API" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(TimeStampAsBigInt), "TimeStampAsBigInt" }
    };
}

namespace ExactOnline.Api.Client.Models;
public partial class WorkflowRequestAttachments
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Division), "Division" },
        { nameof(DownloadUrl), "DownloadUrl" },
        { nameof(FileName), "FileName" },
        { nameof(FileSize), "FileSize" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Request), "Request" }
    };
}

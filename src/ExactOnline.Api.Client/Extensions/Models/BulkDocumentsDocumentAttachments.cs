namespace ExactOnline.Api.Client.Models;
public partial class BulkDocumentsDocumentAttachments
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Attachment), "Attachment" },
        { nameof(Document), "Document" },
        { nameof(FileName), "FileName" },
        { nameof(FileSize), "FileSize" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Url), "Url" }
    };
}

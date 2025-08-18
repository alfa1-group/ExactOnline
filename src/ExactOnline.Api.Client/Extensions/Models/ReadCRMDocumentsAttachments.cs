namespace ExactOnline.Api.Client.Models;
public partial class ReadCRMDocumentsAttachments
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(AttachmentFileName), "AttachmentFileName" },
        { nameof(AttachmentFileSize), "AttachmentFileSize" },
        { nameof(AttachmentUrl), "AttachmentUrl" },
        { nameof(CanShowInWebView), "CanShowInWebView" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" }
    };
}

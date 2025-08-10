using System.ComponentModel.DataAnnotations;

namespace ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs.Options;

public class ExactOnlineAzureBlobStorageOptions
{
    [Required]
    public string ConnectionString { get; set; } = null!;

    [Required]
    public string BlobContainerName { get; set; } = null!;

    [Required] 
    public string FilePath { get; set; } = "Exact/refreshtoken.txt";
}
using System.ComponentModel.DataAnnotations;

namespace ExactOnline.Api.Client.Authentication.Storage.Azure.Blobs.Options;

public class ExactOnlineAzureBlobStorageOptions
{
    [Required]
    public string ConnectionString { get; set; } = null!;

    [Required]
    public string ContainerName { get; set; } = null!;

    [Required] 
    public string RefreshTokenFilePath { get; set; } = "Exact/refreshtoken.txt";
}
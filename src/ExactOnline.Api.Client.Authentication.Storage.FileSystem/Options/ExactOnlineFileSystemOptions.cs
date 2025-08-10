using System.ComponentModel.DataAnnotations;

namespace ExactOnline.Api.Client.Authentication.Storage.FileSystem.Options;

public class ExactOnlineFileSystemOptions
{

    [Required]
    public string RefreshTokenFilePath { get; set; } = null!;
}
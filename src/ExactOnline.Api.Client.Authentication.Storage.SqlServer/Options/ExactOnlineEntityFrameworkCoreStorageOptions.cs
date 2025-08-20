using System.ComponentModel.DataAnnotations;

namespace ExactOnline.Api.Client.Authentication.Storage.SqlServer.Options;

public class ExactOnlineEntityFrameworkCoreStorageOptions
{
    [Required]
    public string ConnectionStringName { get; set; } = "ExactTokens";

    [Required]
    public string TableName { get; set; } = "Exact";

    [Required]
    public string RefreshTokenColumnName { get; set; } = "RefreshToken";

    [Required]
    public string RefreshTokenUpdatedAtColumnName { get; set; } = "RefreshTokenUpdatedAt";

    [Required]
    public string AccessTokenColumnName { get; set; } = "AccessToken";

    [Required]
    public string AccessTokenUpdatedAtColumnName { get; set; } = "AccessTokenUpdatedAt";

    [Required]
    public string AccessTokenExpireColumnName { get; set; } = "AccessTokenExpire";
}
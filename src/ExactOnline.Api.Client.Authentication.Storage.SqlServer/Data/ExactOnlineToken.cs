using System.ComponentModel.DataAnnotations;

namespace ExactOnline.Api.Client.Authentication.Storage.SqlServer.Data;

public class ExactOnlineToken
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(1024)]
    public string RefreshToken { get; set; } = null!;

    public DateTimeOffset RefreshTokenUpdatedAt { get; set; } = DateTimeOffset.MinValue;
}
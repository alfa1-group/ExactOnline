namespace ExactOnline.Api.Client.Models;

public class ExactOnlineEvent
{
    public EventContent Content { get; set; } = null!;

    /// <summary>
    /// Hash code (HMAC SHA256) is a byte array of length 40.
    /// </summary>
    public byte[] HashCode { get; set; } = null!;
}
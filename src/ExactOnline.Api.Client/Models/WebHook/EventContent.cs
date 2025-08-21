namespace ExactOnline.Api.Client.Models.WebHook;

public class EventContent
{
    public string Topic { get; set; } = null!;

    public string Action { get; set; } = null!;

    public int Division { get; set; }

    /// <summary>
    /// ID of the account
    /// </summary>
    public string Key { get; set; } = null!;

    public string ExactOnlineEndpoint { get; set; } = null!;

    public DateTime EventCreatedOn { get; set; }
}
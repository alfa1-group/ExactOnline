namespace ExactOnline.Api.Client.Models.WebHook;

public static class EventActions
{
    /// <summary>
    /// Used when entity data is modified.
    /// </summary>
    public const string UPDATE = "UPDATE";

    /// <summary>
    /// Used when the entity is deleted.
    /// </summary>
    public const string DELETE = "DELETE";
}
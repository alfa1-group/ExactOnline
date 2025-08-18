namespace ExactOnline.Api.Client.Models;
public partial class WebhooksWebhookSubscriptions
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(CallbackURL), "CallbackURL" },
        { nameof(ClientID), "ClientID" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(Topic), "Topic" },
        { nameof(UserID), "UserID" }
    };
}

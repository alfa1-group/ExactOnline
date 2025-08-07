using System;
using System.ComponentModel.DataAnnotations;

namespace ExactOnline.Api.Client.Authentication.Options;

#nullable disable
public class ExactIntegrationOptions
{
    /// <summary>
    /// Instance of Exact to use, for NL this setting doesn't need to be changed)
    /// </summary>
    public string Instance { get; set; } = "https://start.exactonline.nl";

    /// <summary>
    /// Guid used by the application to uniquely identify itself to Exact Online
    /// </summary>
    [Required]
    public string ClientId { get; set; }

    /// <summary>
    /// Client secret (application password)
    /// </summary>
    [Required]
    public string ClientSecret { get; set; }

    /// <summary>
    /// Division ID of the Exact division, can be found by 
    /// issuing a GET request on the Exact API to /api/v1/current/Me?$select=CurrentDivision
    /// </summary>
    [Required]
    public int Division { get; set; }

    /// <summary>
    /// ID of the purchase mailbox to send messages to, can be found by issuing a GET request 
    /// to Exact API /api/v1/{division}/mailbox/Mailboxes
    /// </summary>
    [Required]
    public Guid PurchaseMailboxId { get; set; }

    /// <summary>
    /// Subject of the mail which is sent to the Exact digital postbox
    /// </summary>
    [Required]
    public string MailSubject { get; set; }
}

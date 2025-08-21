using System.ComponentModel.DataAnnotations;

namespace ExactOnline.Api.Client.Authentication.Options;

public class ExactOnlineOptions
{
    /// <summary>
    /// Instance of Exact to use, for NL this setting doesn't need to be changed.
    /// </summary>
    [Required]
    [Url]
    public string BaseUrl { get; set; } = "https://start.exactonline.nl";

    /// <summary>
    /// Guid used by the application to uniquely identify itself to Exact Online.
    /// </summary>
    [Required]
    public string ClientId { get; set; } = null!;

    /// <summary>
    /// Client secret (application password).
    /// </summary>
    [Required]
    public string ClientSecret { get; set; } = null!;
}
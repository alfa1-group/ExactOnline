using System.ComponentModel.DataAnnotations;

namespace ExactOnline.Api.Client.Models;

public class ExactOnlineOptions
{
    /// <summary>
    /// Instance of Exact to use, for NL this setting doesn't need to be changed)
    /// </summary>
    [Required]
    [Url]
    public string BaseUrl { get; set; } = null!;

    /// <summary>
    /// Guid used by the application to uniquely identify itself to Exact Online
    /// </summary>
    [Required]
    public string ClientId { get; set; } = null!;

    /// <summary>
    /// Client secret (application password)
    /// </summary>
    [Required]
    public string ClientSecret { get; set; } = null!;

    /// <summary>
    /// Division ID of the Exact division, can be found by issuing a GET request on the Exact API to /api/v1/current/Me?$select=CurrentDivision
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int Division { get; set; }
}
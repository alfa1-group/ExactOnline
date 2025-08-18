namespace ExactOnline.Api.Client.Models;
public partial class CRMAddressStates
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(Country), "Country" },
        { nameof(DisplayValue), "DisplayValue" },
        { nameof(ID), "ID" },
        { nameof(Latitude), "Latitude" },
        { nameof(Longitude), "Longitude" },
        { nameof(Metadata), "__metadata" },
        { nameof(Name), "Name" },
        { nameof(State), "State" }
    };
}

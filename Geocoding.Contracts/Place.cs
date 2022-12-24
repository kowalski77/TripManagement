namespace Geocoding.Contracts;

public class Place
{
    public List<AddressComponent>? AddressComponents { get; set; }

    public string? FormatAddress { get; set; }

    public Geometry? Geometry { get; set; }

    public string? PlaceId { get; set; }
    
    public List<string>? Types { get; set; }
}

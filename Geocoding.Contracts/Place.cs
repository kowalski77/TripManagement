namespace Geocoding.Contracts;

public class Place
{
    public required AddressComponent AddressComponent { get; set; }

    public required Geometry Geometry { get; set; }

    public required string PlaceId { get; set; }
}

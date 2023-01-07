namespace Geocoding.Contracts;

public class Geometry
{
    public required Location Location { get; set; }

    public string? LocationType { get; set; }
}

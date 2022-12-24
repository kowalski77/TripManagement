namespace Geocoding.Contracts;

public class AddressComponent
{
    public required string Name { get; set; }

    public required string City { get; set; }

    public required string PostalCode { get; set; }
}

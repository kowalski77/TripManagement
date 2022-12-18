#pragma warning disable 8618
using Arch.SharedKernel;
using TripManagement.Domain.Types.Coordinates;

namespace TripManagement.Domain.Types.Locations;

public sealed class Location
{
    private Location() { }

    private Location(Address address, City city, PlaceId placeId, Coordinate coordinate)
    {
        Address = address.NonNull();
        City = city.NonNull();
        PlaceId = placeId.NonNull();
        Coordinate = coordinate.NonNull();
    }

    public static Location Create(Address address, City city, PlaceId placeId, Coordinate coordinate) =>
        new(address, city, placeId, coordinate);

    public Guid Id { get; private set; } = Guid.NewGuid();

    public Address Address { get; private set; }

    public PlaceId PlaceId { get; private set; }

    public City City { get; private set; }

    public Coordinate Coordinate { get; private set; }
}

public record Address(string Value);
public record PlaceId(string Value);
public record City(string Value);

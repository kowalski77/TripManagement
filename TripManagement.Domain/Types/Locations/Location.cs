using TripManagement.Domain.Types.Coordinates;

namespace TripManagement.Domain.Types.Locations;

public sealed record Location(Guid Id, Address Address, City City, PlaceId PlaceId, Coordinate Coordinate)
{
    private Location() : this(Guid.Empty, default!, default!, default!, default!) { }

    public static Location Create(Guid id, Address address, City city, PlaceId placeId, Coordinate coordinate) =>
        new(id, address, city, placeId, coordinate);
}

public record Address(string Value);

public record PlaceId(string Value);

public record City(string Value);

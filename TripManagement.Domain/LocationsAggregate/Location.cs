#pragma warning disable 8618
using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using TripManagement.Domain.Common.Coordinates;

namespace TripManagement.Domain.LocationsAggregate;

public sealed class Location : Entity, IAggregateRoot
{
    private Location() { }

    private Location(Address address, City city, PlaceId placeId, Coordinates coordinates)
    {
        Address = address.NonNull();
        City = city.NonNull();
        PlaceId = placeId.NonNull();
        Coordinates = coordinates.NonNull();
    }

    public static Location Create(Address address, City city, PlaceId placeId, Coordinates coordinates) =>
        new(address, city, placeId, coordinates);

    public Guid Id { get; private set; } = Guid.NewGuid();

    public Address Address { get; private set; }

    public PlaceId PlaceId { get; private set; }

    public City City { get; private set; }

    public Coordinates Coordinates { get; private set; }
}

public record Address(string Value);
public record PlaceId(string Value);
public record City(string Value);

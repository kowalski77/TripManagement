#pragma warning disable 8618
using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.LocationsAggregate;

public record Address(string Value);
public record PlaceId(string Value);
public record City(string Value);

public sealed class Location : Entity, IAggregateRoot
{
    private Location() { }

    private Location(Address address, City city, Coordinates coordinates)
    {
        Address = address.NonNull();
        City = city.NonNull();
        Coordinates = coordinates.NonNull();
    }

    public static Location Create(Address address, City city, Coordinates coordinates) =>
        new(address, city, coordinates);

    public Guid Id { get; private set; } = Guid.NewGuid();

    public Address Address { get; private set; }

    public PlaceId PlaceId { get; private set; }

    public City City { get; private set; }

    public Coordinates Coordinates { get; private set; }
}

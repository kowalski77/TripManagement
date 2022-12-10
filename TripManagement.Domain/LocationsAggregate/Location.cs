#pragma warning disable 8618
using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.LocationsAggregate;

public record Address(string Value);
public record PlaceId(string Value);

public sealed class Location : Entity, IAggregateRoot
{
    private Location() { }

    private Location(Address address, Coordinates coordinates)
    {
        Address = address.NonNull();
        Coordinates = coordinates.NonNull();
    }

    public static Location Create(Address address, Coordinates coordinates) =>
        new(address, coordinates);

    public Guid Id { get; private set; } = Guid.NewGuid();

    public Address Address { get; private set; }

    public PlaceId PlaceId { get; private set; }

    public Coordinates Coordinates { get; private set; }
}

#pragma warning disable 8618
using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using Arch.SharedKernel.Results;
using TripManagement.Domain.CitiesAggregate;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.TripsAggregate;

public record LocationName(string Value)
{
    public static LocationName Create(string value) => new(value);
}

public sealed class Location : Entity
{
    private Location() { }

    public Location(Guid id, string name, City city, Coordinates coordinates)
    {
        Id = id;
        Name = name.NonNullOrEmpty();
        City = city.NonNull();
        Coordinates = coordinates.NonNull();
    }

    public static Result<Location> Create(Guid id, LocationName locationName, City city, Coordinates coordinates) =>
        string.IsNullOrEmpty(locationName.Value) ?
        new ErrorResult("", "") :
            new Location(id, locationName.Value, city, coordinates);


    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; }

    public City City { get; private set; }

    public Coordinates Coordinates { get; private set; }
}

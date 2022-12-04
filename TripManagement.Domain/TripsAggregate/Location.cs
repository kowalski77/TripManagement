#pragma warning disable 8618
using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using Arch.SharedKernel.Results;
using TripManagement.Domain.CitiesAggregate;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.TripsAggregate;

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

    public static Result<Location> Create(Guid id, Maybe<string> maybeName, City city, Coordinates coordinates) =>
        maybeName.HasNoValue ?
        new ErrorResult("", "") :
            new Location(id, maybeName.Value, city, coordinates);


    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; }

    public City City { get; private set; }

    public Coordinates Coordinates { get; private set; }
}

using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using Arch.SharedKernel.Results;

namespace TripManagement.Domain.CitiesAggregate;

public class City : Entity, IAggregateRoot
{
    public City(string name)
    {
        Name = name.NonNullOrEmpty();
        Active = true;
    }

    public static Result<City> Create(Maybe<string> maybeName) => 
        maybeName.HasNoValue ? 
            CityErrors.CityNameNullOrEmpty() : 
            Result.Ok(new City(maybeName.Value));

    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; }

    public bool Active { get; private set; }
}

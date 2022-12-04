using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;

namespace TripManagement.Domain.CitiesAggregate;

public class City : Entity, IAggregateRoot
{
    public City(string name)
    {
        this.Name = name.NonNullOrEmpty();
        this.Active = true;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; }

    public bool Active { get; private set; }
}

#pragma warning disable 8618
using Arch.SharedKernel.DomainDriven;

namespace TripManagement.Domain.DriversAggregate;

public sealed class Car : Entity
{
    private Car() { }

    public Car(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }
}

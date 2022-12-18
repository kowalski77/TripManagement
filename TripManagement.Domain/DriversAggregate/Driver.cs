#pragma warning disable 8618
using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using TripManagement.Domain.Common.Coordinates;

namespace TripManagement.Domain.DriversAggregate;

public sealed class Driver : Entity, IAggregateRoot
{
    public Driver() { }

    public Driver(Guid id, string name, string description, Car car)
    {
        Id = id;
        Name = name.NonNullOrEmpty();
        Description = description;
        Car = car.NonNull();
    }

    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public Car Car { get; private set; }

    public Coordinates? CurrentCoordinates { get; private set; }
}

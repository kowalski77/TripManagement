#pragma warning disable 8618
using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using TripManagement.Domain.DriversAggregate;
using TripManagement.Domain.Types;
using TripManagement.Domain.Types.Coordinates;
using TripManagement.Domain.Types.Locations;

namespace TripManagement.Domain.TripsAggregate;

public sealed class Trip : Entity, IAggregateRoot
{
    private Trip() { }

    internal Trip(Guid Id, UserId userId, DateTime pickUp, Location origin, Location destination)
    {
        this.Id = Id;
        this.UserId = userId.NonNull();
        this.PickUp = pickUp;
        this.Origin = origin.NonNull();
        this.Destination = destination.NonNull();
        this.CurrentCoordinate = origin.Coordinate;
        this.TripStatus = TripStatus.Draft;
        this.Distance = new Distance(0);
        this.CreditsCost = this.CalculateCredits(this.Origin, this.Destination);

        this.AddDomainEvent(new TripCreated(this.Id, this.UserId, this.PickUp, this.Origin, this.Destination));
    }

    internal Trip(Trip trip) 
        : this(trip.Id, trip.UserId, trip.PickUp, trip.Origin, trip.Destination)
    {
    }

    public Guid Id { get; private set; }

    public UserId UserId { get; private set; }

    public Driver? Driver { get; private set; }

    public DateTime PickUp { get; private set; }

    public Location Origin { get; private set; }

    public Location Destination { get; private set; }

    public Coordinate CurrentCoordinate { get; private set; }

    public TripStatus TripStatus { get; internal set; }

    public Distance Distance { get; private set; }

    public Credits CreditsCost { get; private init; }

    //public Result CanInvalidate() => TripStatus is TripStatus.Confirmed ?
    //        Result.Ok() :
    //        TripErrors.InvalidateFailed(TripStatus);

    //public void Invalidate()
    //{
    //    Result result = CanInvalidate();
    //    if (result.Failure)
    //    {
    //        throw new TripInvalidationException(result.Error!.Message);
    //    }

    //    TripStatus = TripStatus.Draft;
    //}

    //public Result CanAssignDriver() => TripStatus is TripStatus.Draft or TripStatus.Confirmed ?
    //        Result.Ok() :
    //        TripErrors.DriverAssignedFailed(TripStatus);

    //public void Assign(Driver driver)
    //{
    //    Result result = CanAssignDriver();
    //    if (result.Failure)
    //    {
    //        throw new AssignDriverFailedException(result.Error!.Message);
    //    }

    //    Driver = driver;
    //}
}

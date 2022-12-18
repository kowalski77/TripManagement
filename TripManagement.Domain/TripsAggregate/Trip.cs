﻿#pragma warning disable 8618
using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using Arch.SharedKernel.Results;
using TripManagement.Domain.Common;
using TripManagement.Domain.DriversAggregate;
using TripManagement.Domain.LocationsAggregate;

namespace TripManagement.Domain.TripsAggregate;

public sealed class Trip : Entity, IAggregateRoot
{
    private Trip() { }

    private Trip(Guid Id, UserId userId, DateTime pickUp, Location origin, Location destination)
    {
        this.Id = Id;
        this.UserId = userId.NonNull();
        this.PickUp = pickUp;
        this.Origin = origin.NonNull();
        this.Destination = destination.NonNull();
        this.CurrentCoordinates = origin.Coordinates;
        this.TripStatus = TripStatus.Draft;
        this.Distance = new Distance(0);
        this.CreditsCost = this.CalculateCredits(this.Origin, this.Destination);
    }

    public Guid Id { get; private set; }

    public UserId UserId { get; private set; }

    public Driver? Driver { get; private set; }

    public DateTime PickUp { get; private set; }

    public Location Origin { get; private set; }

    public Location Destination { get; private set; }

    public Coordinates CurrentCoordinates { get; private set; }

    public TripStatus TripStatus { get; private set; }

    public Distance Distance { get; private set; }

    public Credits CreditsCost { get; private init; }

    public static Result<Trip> CreateDraft(Guid id, UserId userId, DateTime pickUp, Location origin, Location destination, TripOptions options)
    {
        Result result = TripExtensions.Validate(origin, destination, options);
        return result.Success ?
            new Trip(id, userId, pickUp, origin, destination) :
            result.Error!;
    }

    //public Result CanConfirm() => TripStatus is TripStatus.Draft ?
    //        Result.Ok() :
    //        TripErrors.ConfirmFailed(TripStatus);

    //public void Confirm()
    //{
    //    Result result = CanConfirm();
    //    if (result.Failure)
    //    {
    //        throw new TripConfirmationException(result.Error!.Message);
    //    }

    //    TripStatus = TripStatus.Confirmed;
    //}

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

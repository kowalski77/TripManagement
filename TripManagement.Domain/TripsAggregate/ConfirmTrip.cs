using Arch.SharedKernel;
using Arch.SharedKernel.Results;

namespace TripManagement.Domain.TripsAggregate;

public static class ConfirmTrip
{
    public static Result CanConfirm(this Trip trip) =>
        trip.NonNull().TripStatus is TripStatus.Draft ?
            Result.Ok() :
            TripErrors.ConfirmFailed(trip.TripStatus);

    public static void Confirm(this Trip trip) =>
        trip.CanConfirm()
            .OnSuccess(() => trip.TripStatus = TripStatus.Confirmed)
            .OnFailure(result => throw new TripConfirmationException(result.Error!.Message));
}

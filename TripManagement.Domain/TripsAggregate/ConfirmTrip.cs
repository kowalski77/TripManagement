using Arch.SharedKernel;
using Arch.SharedKernel.Results;

namespace TripManagement.Domain.TripsAggregate;

public static class ConfirmTrip
{
    public static Result CanConfirm(this Trip trip) =>
        trip.NonNull().TripStatus == TripStatus.Draft ?
        Result.Ok() :
        TripErrors.ConfirmFailed(trip.TripStatus);

    public static Trip Confirm(this Trip trip)
    {
        Result canConfirm = trip.CanConfirm();
        return canConfirm.Success
            ? new Trip(trip)
            {
                TripStatus = TripStatus.Confirmed
            }
            : throw new TripConfirmationException(canConfirm.Error!.Message);
    }
}

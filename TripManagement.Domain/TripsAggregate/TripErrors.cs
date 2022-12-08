using System.Globalization;
using Arch.SharedKernel.Results;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.TripsAggregate;

public static class TripErrors
{
    public static ErrorResult DriverAssignedFailed(TripStatus status)
    {
        return new ErrorResult(
            TripErrorConstants.DriverAssignFailedCode,
            string.Format(CultureInfo.InvariantCulture, TripErrorConstants.DriverAssignFailedMessage, status.ToString()));
    }

    public static ErrorResult MinimumDistanceBetweenLocations(decimal distance)
    {
        return new ErrorResult(
            TripErrorConstants.MinimumDistanceBetweenLocationsCode,
            string.Format(
                CultureInfo.InvariantCulture,
                TripErrorConstants.MinimumDistanceBetweenLocationsMessage,
                distance.ToString(CultureInfo.InvariantCulture)));
    }

    public static ErrorResult ConfirmFailed(TripStatus status)
    {
        return new ErrorResult(
            TripErrorConstants.ConfirmFailedCode,
            string.Format(CultureInfo.InvariantCulture, TripErrorConstants.ConfirmFailedMessage, status.ToString()));
    }

    public static ErrorResult InvalidateFailed(TripStatus status)
    {
        return new ErrorResult(
            TripErrorConstants.InvalidateFailedCode,
            string.Format(CultureInfo.InvariantCulture, TripErrorConstants.InvalidateFailedMessage, status.ToString()));
    }

    public static ErrorResult LocationNotFoundByCoordinates(Coordinates coordinates)
    {
        return new ErrorResult(
            TripErrorConstants.LocationNotFoundByCoordinatesCode,
            string.Format(
                CultureInfo.InvariantCulture,
                TripErrorConstants.LocationNotFoundByCoordinatesMessage,
                coordinates.Latitude.ToString(CultureInfo.InvariantCulture),
                coordinates.Longitude.ToString(CultureInfo.InvariantCulture)));
    }
}

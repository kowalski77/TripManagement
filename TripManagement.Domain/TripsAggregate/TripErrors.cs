using System.Globalization;
using Arch.SharedKernel.Results;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.TripsAggregate;

public static class TripErrors
{
    public static ErrorResult DriverAssignedFailed(TripStatus status) => new(
            TripErrorConstants.DriverAssignFailedCode,
            string.Format(CultureInfo.InvariantCulture, TripErrorConstants.DriverAssignFailedMessage, status.ToString()));

    public static ErrorResult DistanceBetweenLocations(int minDistance, int maxDistance) => new(
            TripErrorConstants.DistanceBetweenLocationsCode,
            string.Format(
                CultureInfo.InvariantCulture,
                TripErrorConstants.DistanceBetweenLocationsMessage, minDistance, maxDistance));

    public static ErrorResult ConfirmFailed(TripStatus status) => new(
            TripErrorConstants.ConfirmFailedCode,
            string.Format(CultureInfo.InvariantCulture, TripErrorConstants.ConfirmFailedMessage, status.ToString()));

    public static ErrorResult InvalidateFailed(TripStatus status) => new(
            TripErrorConstants.InvalidateFailedCode,
            string.Format(CultureInfo.InvariantCulture, TripErrorConstants.InvalidateFailedMessage, status.ToString()));

    public static ErrorResult LocationNotFoundByCoordinates(Coordinates coordinates) => new(
            TripErrorConstants.LocationNotFoundByCoordinatesCode,
            string.Format(
                CultureInfo.InvariantCulture,
                TripErrorConstants.LocationNotFoundByCoordinatesMessage,
                coordinates.Latitude,
                coordinates.Longitude));
}

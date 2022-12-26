using System.Globalization;
using Arch.SharedKernel;
using Arch.SharedKernel.Results;
using TripManagement.Domain.Types.Coordinates;

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

    public static ErrorResult LocationNotFoundWithCoordinates(Coordinate coordinates) => new(
            TripErrorConstants.LocationNotFoundWithCoordinatesCode,
            string.Format(
                CultureInfo.InvariantCulture,
                TripErrorConstants.LocationNotFoundWithCoordinatesMessage,
                coordinates.NonNull().Latitude,
                coordinates.Longitude));

    public static ErrorResult CityNotAllowed(string city) => new(
            TripErrorConstants.CityNotAllowedCode,
            string.Format(CultureInfo.InvariantCulture, TripErrorConstants.CityNotAllowedMessage, city));

    public static ErrorResult TripNotFound(Guid tripId) => new(
            TripErrorConstants.TripNotFoundCode,
            string.Format(CultureInfo.InvariantCulture, TripErrorConstants.TripNotFoundMessage, tripId));
}

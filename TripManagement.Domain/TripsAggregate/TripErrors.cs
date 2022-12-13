﻿using System.Globalization;
using Arch.SharedKernel.Results;
using TripManagement.Domain.Models.Coordinates;

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

    public static ErrorResult LocationNotFoundWithCoordinates(Coordinates coordinates) => new(
            TripErrorConstants.LocationNotFoundWithCoordinatesCode,
            string.Format(
                CultureInfo.InvariantCulture,
                TripErrorConstants.LocationNotFoundWithCoordinatesMessage,
                coordinates.Latitude,
                coordinates.Longitude));

    public static ErrorResult CityNotAllowed(string city) => new(
            TripErrorConstants.CityNotAllowedCode,
            string.Format(CultureInfo.InvariantCulture, TripErrorConstants.CityNotAllowedMessage, city));
}

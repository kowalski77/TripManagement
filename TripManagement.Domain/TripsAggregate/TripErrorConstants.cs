namespace TripManagement.Domain.TripsAggregate;

public static class TripErrorConstants
{
    public const string DriverAssignFailedCode = "driver.not.assigned";
    public const string DriverAssignFailedMessage = "Driver could not be assigned with status trip {0}";

    public const string DistanceBetweenLocationsCode = "distance.between.locations";
    public const string DistanceBetweenLocationsMessage = "The distance between locations must be between {0} and {1} km";

    public const string ConfirmFailedCode = "confirm.not.possible";
    public const string ConfirmFailedMessage = "Can not confirm due trip status is {0}";

    public const string InvalidateFailedCode = "invalidate.not.possible";
    public const string InvalidateFailedMessage = "Can not invalidate due trip status is {0}";

    public const string UserTripNotFoundCode = "usertrip.not.found";
    public const string TripNotFoundMessage = "Trip for user with id {0} not found";

    public const string LocationNotFoundWithCoordinatesCode = "location.not.found.with.coordinates";
    public const string LocationNotFoundWithCoordinatesMessage = "Location not found with coordinates {0} {1}";

    public const string CityNotAllowedCode = "city.not.allowed";
    public const string CityNotAllowedMessage = "City {0} is not allowed";
}

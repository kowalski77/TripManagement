namespace TripManagement.Domain.CitiesAggregate;

public static class CityErrorConstants
{
    public const string CityNameNullOrEmptyCode = "cityname.nullorempty";

    public const string CityNameNullOrEmptyMessage = "the city name is null or empty";

    public const string CityNameNotFoundWithCoordinatesCode = "cityname.not.found.with.coordinates";

    public const string CityNameNotFoundWithCoordinatesMessage = "could not find city with coordinates {0} and {1}";

    public const string CityNotAvailableCode = "city.not.available";

    public const string CityNotAvailableMessage = "city with name {0} not available in the system";
}

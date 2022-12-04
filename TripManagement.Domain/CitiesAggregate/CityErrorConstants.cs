namespace TripManagement.Domain.CitiesAggregate;

public static class CityErrorConstants
{
    public const string CityNameNullOrEmptyCode = "cityname.nullorempty";

    public const string CityNameNullOrEmptyMessage = "the city name is null or empty";

    public const string CityNameNotFoundByCoordinatesCode = "cityname.not.found.by.coordinates";

    public const string CityNameNotFoundByCoordinatesMessage = "could not find city with coordinates {0} and {1} through Agent";

    public const string CityNameNotFoundCode = "cityname.not.found";

    public const string CityNameNotFoundMessage = "could not find city with the name {0} in database";
}

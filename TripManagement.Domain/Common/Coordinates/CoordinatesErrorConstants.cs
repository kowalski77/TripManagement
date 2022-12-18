namespace TripManagement.Domain.Common.Coordinates;

public static class CoordinatesErrorConstants
{
    public const string OutOfRangeCoordinatesCode = "coordinates.out.ofrange";

    public const string OutOfRangeCoordinatesMessage = "{0} should be between {1} and {2}";

    public const string CityNameCode = "cityname.not.retrieved";

    public const string CityNameMessage = "Could not retrieve city name with coordinates longitude {0} and latitude {1}";

    public const string LocationNameCode = "locationname.not.retrieved";

    public const string LocationNameMessage = "Could not retrieve location name with coordinates longitude {0} and latitude {1}";
}

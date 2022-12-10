namespace TripManagement.Domain.Common;

public static class CoordinatesExtensions
{
    private const int EarthRadius = 6371;

    public static int DistanceInKilometersTo(this Coordinates origin, Coordinates destination) => 
        GetKilometers(CalculateWithHarvesineFormula(origin, destination));

    private static int GetKilometers(double c) => (int)(EarthRadius * c);

    private static double CalculateWithHarvesineFormula(Coordinates origin, Coordinates destination)
    {
        var latitude = (destination.Latitude - origin.Latitude).ToRadians();
        var longitude = (destination.Longitude - origin.Longitude).ToRadians();

        var value = (Math.Sin(latitude / 2) * Math.Sin(latitude / 2)) +
                (Math.Cos(origin.Latitude.ToRadians()) * Math.Cos(destination.Latitude.ToRadians()) *
                Math.Sin(longitude / 2) * Math.Sin(longitude / 2));

        return  2 * Math.Asin(Math.Sqrt(value));
    }

    private static double ToRadians(this double degrees) => degrees * (Math.PI / 180);
}

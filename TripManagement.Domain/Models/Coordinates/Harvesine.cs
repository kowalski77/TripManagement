using TripManagement.Domain.Models.Types;

namespace TripManagement.Domain.Models.Coordinates;

internal static class Harvesine
{
    public static Distance CalculateDistanceTo(this Coordinates origin, Coordinates destination) =>
        new(2 * Math.Asin(Math.Sqrt(
            (Math.Sin(GetRadians(destination.Latitude, origin.Latitude) .Value / 2) * Math.Sin(GetRadians(destination.Latitude, origin.Latitude).Value / 2)) +
            (Math.Cos(GetRadians(origin.Latitude).Value) * Math.Cos(GetRadians(destination.Latitude).Value) *
            Math.Sin(GetRadians(destination.Longitude, origin.Longitude).Value / 2) * Math.Sin(GetRadians(destination.Longitude, origin.Longitude).Value / 2)))));
    
    private static Radians GetRadians(double a, double b) => new Degrees(a - b).ToRadians();

    private static Radians GetRadians(double a) => new Degrees(a).ToRadians();
}
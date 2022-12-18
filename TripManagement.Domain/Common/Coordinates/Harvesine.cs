namespace TripManagement.Domain.Common.Coordinates;

internal static class Harvesine
{
    public static Distance CalculateDistanceTo(this Coordinates origin, Coordinates destination)
    {
        Radians latitude = new Degrees(destination.Latitude - origin.Latitude).ToRadians();
        Radians longitude = new Degrees(destination.Longitude - origin.Longitude).ToRadians();

        var value = Math.Sin(latitude.Value / 2) * Math.Sin(latitude.Value / 2) +
                Math.Cos(new Degrees(origin.Latitude).ToRadians().Value) * Math.Cos(new Degrees(destination.Latitude).ToRadians().Value) *
                Math.Sin(longitude.Value / 2) * Math.Sin(longitude.Value / 2);

        return new Distance(2 * Math.Asin(Math.Sqrt(value)));
    }
}
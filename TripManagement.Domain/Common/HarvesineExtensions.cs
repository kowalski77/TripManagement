namespace TripManagement.Domain.Common;

public static class HarvesineExtensions
{
    private const int EarthRadius = 6371;

    public static Kilometers ToKilometers(this Distance c) => new((int)(EarthRadius * c.Value));

    public static Milles ToMilles(this Distance c) => new((int)(EarthRadius * c.Value * 0.621371));

    public static Distance CalculateDistanceTo(this Coordinates origin, Coordinates destination)
    {
        var latitude = new Degrees(destination.Latitude - origin.Latitude).ToRadians().Value;
        var longitude = new Degrees(destination.Longitude - origin.Longitude).ToRadians().Value;

        var value = (Math.Sin(latitude / 2) * Math.Sin(latitude / 2)) +
                (Math.Cos(new Degrees(origin.Latitude).ToRadians().Value) * Math.Cos(new Degrees(destination.Latitude).ToRadians().Value) *
                Math.Sin(longitude / 2) * Math.Sin(longitude / 2));

        return new Distance(2 * Math.Asin(Math.Sqrt(value)));
    }

    private static Radians ToRadians(this Degrees degrees) => new(degrees.Value * (Math.PI / 180));
}

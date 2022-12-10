namespace TripManagement.Domain.Common;

public static class CoordinatesExtensions
{
    public static int DistanceInKilometersTo(this Coordinates origin, Coordinates destination)
    {
        var latitude = origin.Latitude - destination.Latitude;
        var longitude = origin.Longitude - destination.Longitude;

        var a = Math.Pow(Math.Sin(latitude / 2), 2) + Math.Cos(origin.Latitude) * Math.Cos(destination.Latitude) * Math.Pow(Math.Sin(longitude / 2), 2);
        var c = 2 * Math.Asin(Math.Sqrt(a));

        return (int)(6371 * c);
    }
}

namespace TripManagement.Domain.Common;

public static class CoordinatesExtensions
{
    public static Kilometers DistanceInKilometersTo(this Coordinates origin, Coordinates destination) =>
        origin.CalculateDistanceTo(destination).ToKilometers();
}
using TripManagement.Domain.Models.Types;

namespace TripManagement.Domain.Models.Coordinates;

public static class CoordinatesExtensions
{
    public static Kilometers DistanceTo(this Coordinates origin, Coordinates destination) =>
        origin.CalculateDistanceTo(destination).ToKilometers();
}

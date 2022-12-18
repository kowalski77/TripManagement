using Arch.SharedKernel;

namespace TripManagement.Domain.Types.Coordinates;

public static class CoordinatesExtensions
{
    public static Kilometers DistanceTo(this Coordinate origin, Coordinate destination) =>
        origin.NonNull().CalculateDistanceTo(destination.NonNull()).ToKilometers();
}

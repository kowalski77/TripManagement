using Arch.SharedKernel;

namespace TripManagement.Domain.Common;

public static class CoordinatesExtensions
{
    public static Kilometers DistanceTo(this Coordinates origin, Coordinates destination) =>
        origin.NonNull().CalculateDistanceTo(destination.NonNull()).ToKilometers();
}

using TripManagement.Domain.Types.Locations;

namespace TripManagement.Domain.TripsAggregate;

public static class TripCostCalculator
{
    // NOTE: This is a dummy implementation, simulating a complex system
    public static Credits CalculateCredits(this Trip _, Location origin, Location destination)
    {
        var cost = Random.Shared.Next(1, 10);

        return new Credits(cost);
    }
}

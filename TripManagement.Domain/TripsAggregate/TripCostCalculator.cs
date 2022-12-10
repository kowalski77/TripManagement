using TripManagement.Domain.LocationsAggregate;

namespace TripManagement.Domain.TripsAggregate;

public static class TripCostCalculator
{
    // NOTE: This is a dummy implementation, simulating a complex system
    public static int CalculateCredits(this Trip _, Location origin, Location destination)
    {
        Random random = new();
        var cost = random.Next(1, 10);

        return cost;
    }
}

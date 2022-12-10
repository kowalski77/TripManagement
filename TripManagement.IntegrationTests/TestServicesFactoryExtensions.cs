using TripManagement.Infrastructure.Persistence;

namespace TripManagement.IntegrationTests;

public static class TestServicesFactoryExtensions
{
    public static void DeleteAllTrips(this TestServicesFactory factory)
    {
        TripManagementContext dbContext = factory.GetService<TripManagementContext>();
        dbContext.Trips.RemoveRange(dbContext.Trips);
        dbContext.SaveChanges();
    }
}

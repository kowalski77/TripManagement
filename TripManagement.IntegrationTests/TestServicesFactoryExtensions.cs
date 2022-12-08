using TripManagement.Domain.CitiesAggregate;
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

    public static async Task AddCityAsync(this TestServicesFactory factory, string city)
    {
        TripManagementContext dbContext = factory.GetService<TripManagementContext>();
        dbContext.Cities.Add(new City(city));
        await dbContext.SaveChangesAsync();
    }
}

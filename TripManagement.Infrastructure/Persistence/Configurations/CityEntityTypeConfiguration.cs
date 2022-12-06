using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripManagement.Domain.CitiesAggregate;

namespace TripManagement.Infrastructure.Persistence.Configurations;

public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasData(GetInitialCities());
    }

    private static IEnumerable<City> GetInitialCities() => new[] { new City("Sabadell"), new City("Barcelona") };
}

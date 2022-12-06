using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripManagement.Domain.TripsAggregate;

namespace TripManagement.Infrastructure.Persistence.Configurations;

public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.OwnsOne(x => x.Coordinates, y =>
        {
            y.Property(x => x.Latitude).HasColumnName("Latitude");
            y.Property(x => x.Longitude).HasColumnName("Longitude");
        });
    }
}

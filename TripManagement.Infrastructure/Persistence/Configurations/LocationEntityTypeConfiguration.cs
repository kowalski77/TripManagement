using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripManagement.Domain.Types.Locations;

namespace TripManagement.Infrastructure.Persistence.Configurations;

public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.OwnsOne(x => x.Address, y => y.Property(x => x.Value).HasColumnName("Address"));
        builder.OwnsOne(x => x.PlaceId, y => y.Property(x => x.Value).HasColumnName("PlaceId"));
        builder.OwnsOne(x => x.City, y => y.Property(x => x.Value).HasColumnName("City"));
        builder.OwnsOne(x => x.Coordinate, y =>
        {
            y.Property(x => x.Latitude).HasColumnName("Latitude");
            y.Property(x => x.Longitude).HasColumnName("Longitude");
        });
    }
}

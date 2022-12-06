using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripManagement.Domain.DriversAggregate;

namespace TripManagement.Infrastructure.Persistence.Configurations;

public class DriverEntityTypeConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.OwnsOne(x => x.CurrentCoordinates, y =>
        {
            y.Property(z => z.Latitude).HasColumnName("Latitude");
            y.Property(z => z.Longitude).HasColumnName("Longitude");
        });
    }
}

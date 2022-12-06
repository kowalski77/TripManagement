using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripManagement.Domain.TripsAggregate;

namespace TripManagement.Infrastructure.Persistence.Configurations;

public class TripEntityTypeConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.OwnsOne(x => x.UserId, y =>
        {
            y.Property(z => z.Value).HasColumnName("UserId");
        });

        builder.OwnsOne(x => x.CurrentCoordinates, y =>
        {
            y.Property(z => z.Latitude).HasColumnName("Latitude");
            y.Property(z => z.Longitude).HasColumnName("Longitude");
        });

        builder
            .HasOne(x => x.Driver)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(x => x.Destination)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Origin)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}

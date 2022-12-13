namespace TripManagement.Domain.Models.Types;

public record struct Radians(double Value);

public record struct Degrees(double Value)
{
    public Radians ToRadians() => new(Value * (Math.PI / 180));
}

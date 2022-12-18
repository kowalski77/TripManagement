namespace TripManagement.Domain.Common;

public record struct Radians(double Value);

public record struct Degrees(double Value)
{
    public Radians ToRadians() => new(Value * (Math.PI / 180));
}

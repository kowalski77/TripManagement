namespace TripManagement.Domain.Models.Types;

public record struct Radians(double Value);

public record struct Degrees(double Value)
{
    public Radians ToRadians() => new(Value * (Math.PI / 180));
}

public record struct Kilometers(Distance Distance, double Value);

public record struct Milles(Distance Distance, double Value);

public record struct Distance(double Value)
{
    private const int EarthRadius = 6371;

    public Milles ToMilles() => new(this, Value * 0.621371);

    public Kilometers ToKilometers() => new(this, (int)(Value * EarthRadius));
}

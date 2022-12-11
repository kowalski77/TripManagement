using System.ComponentModel.DataAnnotations;

namespace TripManagement.Domain.TripsAggregate;

public class TripOptions
{
    public int MinDistanceBetweenLocations { get; init; } = 1;

    public int MaxDistanceBetweenLocations { get; init; } = 100;

    [Required]
    public required IEnumerable<string> AllowedCities { get; init; }
}

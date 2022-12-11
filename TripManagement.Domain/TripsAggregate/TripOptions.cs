using System.ComponentModel.DataAnnotations;

namespace TripManagement.Domain.TripsAggregate;

public class TripOptions
{
    public int MinDistanceBetweenLocations { get; init; }

    public int MaxDistanceBetweenLocations { get; init; }

    [Required]
    public required IEnumerable<string> AllowedCities { get; init; }
}

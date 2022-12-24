using System.ComponentModel.DataAnnotations;

namespace TripManagement.Infrastructure.Agents;

public class GeocodeApiOptions
{
    [Required]
    public required string BaseUrl { get; init; }

    [Required]
    public required string LocationByCoordinatesEndpoint { get; init; }
}

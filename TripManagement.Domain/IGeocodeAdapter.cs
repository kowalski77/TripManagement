using Arch.SharedKernel.Results;
using TripManagement.Domain.LocationsAggregate;
using TripManagement.Domain.Models.Coordinates;

namespace TripManagement.Domain;

public interface IGeocodeAdapter
{
    Task<Result<Location>> GetLocationByCoordinatesAsync(Coordinates coordinates, CancellationToken cancellationToken = default);
}

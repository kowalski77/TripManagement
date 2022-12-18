using Arch.SharedKernel.Results;
using TripManagement.Domain.Types.Coordinates;
using TripManagement.Domain.Types.Locations;

namespace TripManagement.Domain;

public interface IGeocodeAdapter
{
    Task<Result<Location>> GetLocationByCoordinatesAsync(Coordinate coordinates, CancellationToken cancellationToken = default);
}

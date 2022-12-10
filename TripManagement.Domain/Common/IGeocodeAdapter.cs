using Arch.SharedKernel.Results;
using TripManagement.Domain.LocationsAggregate;

namespace TripManagement.Domain.Common;

public interface IGeocodeAdapter
{
    Task<Result<Location>> GetLocationByCoordinatesAsync(Coordinates coordinates, CancellationToken cancellationToken = default);
}

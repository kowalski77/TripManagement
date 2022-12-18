using Arch.SharedKernel.Results;
using TripManagement.Domain.Common.Coordinates;
using TripManagement.Domain.LocationsAggregate;

namespace TripManagement.Domain;

public interface IGeocodeAdapter
{
    Task<Result<Location>> GetLocationByCoordinatesAsync(Coordinates coordinates, CancellationToken cancellationToken = default);
}

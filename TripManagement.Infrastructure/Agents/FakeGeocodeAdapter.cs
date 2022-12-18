using Arch.SharedKernel;
using Arch.SharedKernel.Results;
using TripManagement.Domain;
using TripManagement.Domain.Types.Coordinates;
using TripManagement.Domain.Types.Locations;

namespace TripManagement.Infrastructure.Agents;

public sealed class FakeGeocodeAdapter : IGeocodeAdapter
{
    // TODO: Fake Agent Service, dummy implementations, replace with Geocoding.API (simulating Google API) since 3rd party agent, handle exceptions and timeouts
    // use functional techniques when mapping from API objects to locations
    public Task<Result<Location>> GetLocationByCoordinatesAsync(Coordinate coordinates, CancellationToken cancellationToken = default) =>
        coordinates.NonNull().Latitude > 0 ?
            Task.FromResult(Result.Ok(Location.Create(Guid.NewGuid(), new Address("Carrer de la Diputació, 261"), new City("Barcelona"), new PlaceId(Guid.NewGuid().ToString()), coordinates))) :
            Task.FromResult(Result.Ok(Location.Create(Guid.NewGuid(), new Address("Carrer de la Creu, 14"), new City("Sabadell"), new PlaceId(Guid.NewGuid().ToString()), coordinates)));
}

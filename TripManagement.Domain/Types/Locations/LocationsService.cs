using Arch.SharedKernel;
using Arch.SharedKernel.Results;
using TripManagement.Domain.Types.Coordinates;

namespace TripManagement.Domain.Types.Locations;

public class LocationsService
{
    private readonly IGeocodeAdapter geocodeAdapter;

    public LocationsService(IGeocodeAdapter geocodeAdapter) => this.geocodeAdapter = geocodeAdapter.NonNull();

    public async Task<Result<(Location origin, Location destination)>> CreateTripLocationsAsync(Coordinate origin, Coordinate destination, CancellationToken cancellationToken = default)
    {
        Result<Location> originLocation = await geocodeAdapter.GetLocationByCoordinatesAsync(origin.NonNull(), cancellationToken).ConfigureAwait(false);
        if (originLocation.Failure)
        {
            return originLocation.Error!;
        }

        Result<Location> destinationLocation = await geocodeAdapter.GetLocationByCoordinatesAsync(destination.NonNull(), cancellationToken).ConfigureAwait(false);

        return destinationLocation.Failure ?
            destinationLocation.Error! :
            (originLocation.Value, destinationLocation.Value);
    }
}

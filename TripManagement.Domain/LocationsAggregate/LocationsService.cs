using Arch.SharedKernel;
using Arch.SharedKernel.Results;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.LocationsAggregate;

public class LocationsService
{
    private readonly IGeocodeAdapter geocodeAdapter;

    public LocationsService(IGeocodeAdapter geocodeAdapter) => this.geocodeAdapter = geocodeAdapter.NonNull();

    public async Task<Result<(Location origin, Location destination)>> CreateTripLocationsAsync(Coordinates origin, Coordinates destination, CancellationToken cancellationToken = default)
    {
        Result<Location> originLocation = await geocodeAdapter.GetLocationByCoordinatesAsync(origin.NonNull(), cancellationToken);
        if (originLocation.Failure)
        {
            return originLocation.Error!;
        }

        Result<Location> destinationLocation = await geocodeAdapter.GetLocationByCoordinatesAsync(destination.NonNull(), cancellationToken);

        return destinationLocation.Failure ?
            destinationLocation.Error! :
            (originLocation.Value, destinationLocation.Value);
    }
}

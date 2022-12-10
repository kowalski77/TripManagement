using Arch.SharedKernel;
using Arch.SharedKernel.Results;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.LocationsAggregate;

public class LocationsService
{
    private readonly IGeocodeAdapter geocodeAdapter;

    public LocationsService(IGeocodeAdapter geocodeAdapter)
    {
        this.geocodeAdapter = geocodeAdapter.NonNull();
    }

    public async Task<Result<(Location origin, Location destination)>> CreateTripLocationsAsync(Coordinates origin, Coordinates destination, CancellationToken cancellationToken = default)
    {
        Result<Location> originLocation = await CreateAsync(origin, cancellationToken);
        if (originLocation.Failure)
        {
            return originLocation.Error!;
        }

        Result<Location> destinationLocation = await CreateAsync(destination, cancellationToken);

        return destinationLocation.Failure ?
            destinationLocation.Error! :
            (originLocation.Value, destinationLocation.Value);
    }

    private async Task<Result<Location>> CreateAsync(Coordinates coordinates, CancellationToken cancellationToken = default)
    {
        //Maybe<string> maybeCityName = await geocodeAdapter.GetCityByCoordinatesAsync(coordinates, cancellationToken);
        //if (maybeCityName.HasNoValue)
        //{
        //    return CityErrors.CityNotFoundWithCoordinates(coordinates);
        //}

        //Maybe<City> maybeCity = await cityRepository.GetCityByNameAsync(maybeCityName.Value, cancellationToken);
        //if (maybeCity.HasNoValue)
        //{
        //    return CityErrors.CityNotAvailable(maybeCityName.Value);
        //}

        //Maybe<string> maybeLocationName = await geocodeAdapter.GetLocationByCoordinatesAsync(coordinates, cancellationToken);
        //if (maybeLocationName.HasNoValue)
        //{
        //    return TripErrors.LocationNotFoundWithCoordinates(coordinates);
        //}

        //return Location.Create(
        //    Guid.NewGuid(), 
        //    LocationName.Create(maybeLocationName.Value), 
        //    maybeCity.Value, 
        //    coordinates);

        return null;
    }
}

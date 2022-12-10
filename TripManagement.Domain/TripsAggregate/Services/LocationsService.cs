using Arch.SharedKernel;
using Arch.SharedKernel.Results;
using TripManagement.Domain.CitiesAggregate;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.TripsAggregate.Services;

public class LocationsService
{
    private readonly ICoordinatesAgent coordinatesAgent;
    private readonly ICitiesRepository cityRepository;

    public LocationsService(ICoordinatesAgent coordinatesAgent, ICitiesRepository cityRepository)
    {
        this.coordinatesAgent = coordinatesAgent.NonNull();
        this.cityRepository = cityRepository.NonNull();
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
        Maybe<string> maybeCityName = await coordinatesAgent.GetCityByCoordinatesAsync(coordinates, cancellationToken);
        if (maybeCityName.HasNoValue)
        {
            return CityErrors.CityNotFoundWithCoordinates(coordinates);
        }
        
        Maybe<City> maybeCity = await cityRepository.GetCityByNameAsync(maybeCityName.Value, cancellationToken);
        if (maybeCity.HasNoValue)
        {
            return CityErrors.CityNotAvailable(maybeCityName.Value);
        }

        Maybe<string> maybeLocationName = await coordinatesAgent.GetLocationByCoordinatesAsync(coordinates, cancellationToken);
        if (maybeLocationName.HasNoValue)
        {
            return TripErrors.LocationNotFoundWithCoordinates(coordinates);
        }

        return Location.Create(
            Guid.NewGuid(), 
            LocationName.Create(maybeLocationName.Value), 
            maybeCity.Value, 
            coordinates);
    }
}

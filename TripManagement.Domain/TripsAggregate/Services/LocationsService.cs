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
        return destinationLocation.Failure ? destinationLocation.Error! : (originLocation.Value, destinationLocation.Value);
    }

    private async Task<Result<Location>> CreateAsync(Coordinates coordinates, CancellationToken cancellationToken = default)
    {
        Task<Maybe<string>> maybeCityNameTask = coordinatesAgent.GetCityByCoordinatesAsync(coordinates, cancellationToken);
        Task<Maybe<string>> maybeLocationNameTask = coordinatesAgent.GetLocationByCoordinatesAsync(coordinates, cancellationToken);

        await Task.WhenAll(maybeCityNameTask, maybeLocationNameTask);

        Maybe<string> cityName = await maybeCityNameTask;
        if (cityName.HasNoValue)
        {
            return CityErrors.CityNotFoundByCoordinates(coordinates);
        }
        
        Maybe<City> maybeCity = await cityRepository.GetCityByNameAsync(cityName.Value, cancellationToken);
        if (maybeCity.HasValue)
        {
            return CityErrors.CityNotFoundByName(cityName.Value);
        }

        return Location.Create(Guid.NewGuid(), await maybeLocationNameTask, maybeCity.Value, coordinates);
    }
}

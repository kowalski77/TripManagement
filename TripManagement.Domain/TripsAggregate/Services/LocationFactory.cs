using Arch.SharedKernel;
using Arch.SharedKernel.Results;
using TripManagement.Domain.CitiesAggregate;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.TripsAggregate.Services;

public class LocationFactory
{
    private readonly ICoordinatesAgent coordinatesAgent;
    private readonly ICityRepository cityRepository;

    public LocationFactory(ICoordinatesAgent coordinatesAgent, ICityRepository cityRepository)
    {
        this.coordinatesAgent = coordinatesAgent.NonNull();
        this.cityRepository = cityRepository.NonNull();
    }

    public async Task<Result<Location>> CreateAsync(Coordinates coordinates, CancellationToken cancellationToken = default)
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

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

        Maybe<string> maybeCityName = await maybeCityNameTask;
        if (maybeCityName.HasNoValue)
        {
            return CoordinatesErrors.CityNameNotRetrieved(coordinates);
        }

        Maybe<string> maybeLocationName = await maybeLocationNameTask;
        if (maybeLocationName.HasNoValue)
        {
            return CoordinatesErrors.LocationNameNotRetrieved(coordinates);
        }

        Maybe<City> maybeCity = await cityRepository.GetCityByNameAsync(maybeCityName.Value, cancellationToken);
        return maybeCity.HasNoValue
            ? CityErrors.CityNotFoundByName(maybeCityName.Value)
            : new Location(Guid.NewGuid(), maybeLocationName.Value, maybeCity.Value, coordinates);
    }
}

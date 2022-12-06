using Arch.SharedKernel;

namespace TripManagement.Domain.CitiesAggregate;

public interface ICitiesRepository
{
    Task<Maybe<City>> GetCityByNameAsync(string name, CancellationToken cancellationToken = default);
}

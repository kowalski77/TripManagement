using Arch.SharedKernel;

namespace TripManagement.Domain.CitiesAggregate;

public interface ICityRepository
{
    Task<Maybe<City>> GetCityByNameAsync(string name, CancellationToken cancellationToken = default);
}

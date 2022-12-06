using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using TripManagement.Domain.CitiesAggregate;

namespace TripManagement.Infrastructure.Persistence;

internal class CitiesRepository : BaseRepository<City>, ICitiesRepository
{
    public CitiesRepository(TripManagementContext context) : base(context)
    {
    }

    public Task<Maybe<City>> GetCityByNameAsync(string name, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}

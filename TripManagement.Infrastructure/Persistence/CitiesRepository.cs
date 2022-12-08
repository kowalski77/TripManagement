using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using Microsoft.EntityFrameworkCore;
using TripManagement.Domain.CitiesAggregate;

namespace TripManagement.Infrastructure.Persistence;

internal class CitiesRepository : BaseRepository<City>, ICitiesRepository
{
    private readonly TripManagementContext context;

    public CitiesRepository(TripManagementContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<Maybe<City>> GetCityByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await this.context.Cities.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
}

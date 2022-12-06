using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using TripManagement.Domain.TripsAggregate;

namespace TripManagement.Infrastructure.Persistence
{
    internal class TripsRepository : BaseRepository<Trip>, ITripRepository
    {
        private readonly TripManagementContext context;

        public TripsRepository(TripManagementContext context) : base(context) => this.context = context;

        public override async Task<Maybe<Trip>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Trip? trip = await context.Trips.FindAsync(new object?[] { id }, cancellationToken).ConfigureAwait(false);
            if (trip is null)
            {
                return trip;
            }

            await Context.Entry(trip).Reference(x => x.Origin).LoadAsync(cancellationToken).ConfigureAwait(false);
            await context.Entry(trip).Reference(x => x.Destination).LoadAsync(cancellationToken).ConfigureAwait(false);
            await context.Entry(trip).Reference(x => x.Driver).LoadAsync(cancellationToken).ConfigureAwait(false);

            return trip;
        }
    }
}

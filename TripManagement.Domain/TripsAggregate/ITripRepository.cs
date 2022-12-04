using Arch.SharedKernel.DomainDriven;

namespace TripManagement.Domain.TripsAggregate;

public interface ITripRepository : IRepository<Trip>
{
}

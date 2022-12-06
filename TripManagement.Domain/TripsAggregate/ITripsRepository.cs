using Arch.SharedKernel.DomainDriven;

namespace TripManagement.Domain.TripsAggregate;

public interface ITripsRepository : IRepository<Trip>
{
}

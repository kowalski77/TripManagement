using Arch.SharedKernel.DomainDriven;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TripManagement.Domain.TripsAggregate;

namespace TripManagement.Infrastructure.Persistence;

public class TripManagementContext : TransactionContext
{
    public TripManagementContext(DbContextOptions<TripManagementContext> options, IMediator mediator)
        : base(options, mediator)
    {
    }
    
    public DbSet<Trip> Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TripManagementContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

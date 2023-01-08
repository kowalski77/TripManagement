using Arch.SharedKernel.DomainDriven;
using Arch.SharedKernel.Events;
using Arch.SharedKernel.Outbox;
using MediatR;
using TripManagement.Application.Behaviors;
using TripManagement.Application.Trips.DraftTrip;

namespace TripManagement.Application;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Handler).Assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
        services.AddScoped<IEventBusAdapter, EventBusAdapter>();
        services.AddScoped(sp => new OutboxService(
            sp.GetRequiredService<IDbContext>(),
            sp.GetRequiredService<IEventBusAdapter>(),
            dc => new OutboxRepository(dc)));
    }
}

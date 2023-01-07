using Arch.SharedKernel.Events;
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
    }
}

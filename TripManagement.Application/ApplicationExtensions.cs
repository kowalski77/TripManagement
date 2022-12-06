using MediatR;
using TripManagement.Application.Behaviors;
using TripManagement.Application.Trips.CreateDraft;

namespace TripManagement.Application;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Handler).Assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
    }
}

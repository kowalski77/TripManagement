using Arch.SharedKernel.Events;
using MediatR;
using TripManagement.Domain.TripsAggregate;
using TripCreatedIntegrationEvent = TripManagement.Contracts.TripCreated;

namespace TripManagement.Application.Trips.Events;

public sealed class TripCreatedHandler : INotificationHandler<TripCreated>
{
    private readonly IEventBusAdapter eventBusAdapter;

    public TripCreatedHandler(IEventBusAdapter eventBusAdapter) => this.eventBusAdapter = eventBusAdapter;

    public async Task Handle(TripCreated notification, CancellationToken cancellationToken)
    {
        TripCreatedIntegrationEvent tripCreated = new(
            notification.Id, 
            notification.UserId.Value, 
            notification.PickUp, 
            notification.Origin.City.Value, 
            notification.Destination.City.Value);

        await eventBusAdapter.PublishAsync(tripCreated, cancellationToken);
    }
}

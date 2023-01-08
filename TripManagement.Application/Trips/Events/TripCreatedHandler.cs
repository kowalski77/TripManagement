using Arch.SharedKernel.Outbox;
using MediatR;
using TripManagement.Domain.TripsAggregate;
using TripCreatedIntegrationEvent = TripManagement.Contracts.TripCreated;

namespace TripManagement.Application.Trips.Events;

public sealed class TripCreatedHandler : INotificationHandler<TripCreated>
{
    private readonly OutboxService outboxService;

    public TripCreatedHandler(OutboxService outboxService) => this.outboxService = outboxService;

    public async Task Handle(TripCreated notification, CancellationToken cancellationToken)
    {
        TripCreatedIntegrationEvent tripCreated = new(
            notification.Id, 
            notification.UserId.Value, 
            notification.PickUp, 
            notification.Origin.City.Value, 
            notification.Destination.City.Value);

        await this.outboxService.AddIntegrationEventAsync(tripCreated, cancellationToken);
    }
}

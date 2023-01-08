using System.Data.Common;
using Arch.SharedKernel.DomainDriven;
using Arch.SharedKernel.Events;
using Microsoft.EntityFrameworkCore;

namespace Arch.SharedKernel.Outbox;

public sealed class OutboxService
{
    private readonly IDbContext context;
    private readonly IEventBusAdapter eventBusAdapter;
    private readonly OutboxRepository outboxRepository;

    public OutboxService(IDbContext context, IEventBusAdapter eventBusAdapter, Func<DbConnection, OutboxRepository> outboxRepositoryFactory)
    {
        this.context = context;
        this.eventBusAdapter = eventBusAdapter;
        this.outboxRepository = outboxRepositoryFactory.NonNull()(context.NonNull().DatabaseFacade.GetDbConnection());
    }

    public async Task AddIntegrationEventAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
        await this.outboxRepository.SaveMessageAsync(integrationEvent.NonNull(), this.context.GetCurrentTransaction(), cancellationToken).ConfigureAwait(false);

    public async Task PublishIntegrationEventsAsync(Guid transactionId, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<OutboxMessage> pendingOutboxMessages = await this.outboxRepository.GetNotPublishedAsync(transactionId, cancellationToken).ConfigureAwait(false);
        foreach (OutboxMessage outboxMessage in pendingOutboxMessages)
        {
            await this.TryPublishIntegrationEventsAsync(outboxMessage, cancellationToken).ConfigureAwait(false);
        }
    }

    public async Task PublishPendingIntegrationEventsAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<OutboxMessage> pendingOutboxMessages = await this.outboxRepository.GetNotPublishedAsync(cancellationToken).ConfigureAwait(false);
        foreach (OutboxMessage outboxMessage in pendingOutboxMessages)
        {
            await this.TryPublishIntegrationEventsAsync(outboxMessage, cancellationToken).ConfigureAwait(false);
        }
    }

    private async Task TryPublishIntegrationEventsAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken = default)
    {
        try
        {
            var message = await outboxMessage.DeserializeAsync().ConfigureAwait(false);

            await this.eventBusAdapter.PublishAsync(message, message.GetType(), cancellationToken).ConfigureAwait(false);

            await this.outboxRepository.MarkMessageAsPublishedAsync(outboxMessage.Id, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            await this.outboxRepository.MarkMessageAsFailedAsync(outboxMessage.Id, cancellationToken).ConfigureAwait(false);
        }
    }
}

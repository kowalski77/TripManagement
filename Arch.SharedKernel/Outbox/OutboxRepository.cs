using System.Data.Common;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Arch.SharedKernel.Outbox;

public sealed class OutboxRepository : IDisposable
{
    private readonly OutboxContext context;

    public OutboxRepository(DbConnection dbConnection) => context = new OutboxContext(new DbContextOptionsBuilder<OutboxContext>()
            .UseSqlServer(dbConnection).Options);

    public async Task SaveMessageAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, IDbContextTransaction transaction, CancellationToken cancellationToken = default)
    {
        await context.Database.UseTransactionAsync(transaction.GetDbTransaction(), cancellationToken).ConfigureAwait(false);

        OutboxMessage outboxMessage = GetOutboxMessage(integrationEvent.NonNull(), transaction.NonNull().TransactionId);
        await context.AddAsync(outboxMessage, cancellationToken).ConfigureAwait(false);
        await context.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task MarkMessageAsPublishedAsync(Guid messageId, CancellationToken cancellationToken = default) =>
        await UpdateStatusAsync(messageId, EventState.Published, cancellationToken).ConfigureAwait(false);

    public async Task MarkMessageAsFailedAsync(Guid messageId, CancellationToken cancellationToken = default) =>
        await UpdateStatusAsync(messageId, EventState.PublishedFailed, cancellationToken).ConfigureAwait(false);

    public async Task<IReadOnlyList<OutboxMessage>> GetNotPublishedAsync(Guid transactionId, CancellationToken cancellationToken = default) =>
        (await context.OutboxMessages
            .Where(e => e.TransactionId == transactionId && e.State == EventState.NotPublished)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false))
        .AsReadOnly();

    public async Task<IReadOnlyList<OutboxMessage>> GetNotPublishedAsync(CancellationToken cancellationToken = default) =>
        (await context.OutboxMessages
            .Where(e => e.State == EventState.NotPublished || e.State == EventState.PublishedFailed)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false))
        .AsReadOnly();

    private async Task UpdateStatusAsync(Guid messageId, EventState eventState, CancellationToken cancellationToken = default)
    {
        OutboxMessage message = await context.OutboxMessages.FirstAsync(x => x.Id == messageId, cancellationToken).ConfigureAwait(false);
        message.State = eventState;

        context.OutboxMessages.Update(message);

        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private static OutboxMessage GetOutboxMessage<TIntegrationEvent>(TIntegrationEvent integrationEvent, Guid transactionId)
    {
        Type integrationEventType = integrationEvent.NonNull()!.GetType();
        var data = JsonSerializer.Serialize(integrationEvent, integrationEventType);
        
        return new(
            transactionId, 
            DateTime.Now,
            integrationEventType,
            data);
    }

    public void Dispose() => context.Dispose();
}

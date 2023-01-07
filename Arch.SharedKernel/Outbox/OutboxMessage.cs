namespace Arch.SharedKernel.Outbox;

public sealed class OutboxMessage
{
    public Guid Id { get; private set; }

    public Guid TransactionId { get; private set; }

    public DateTime OccurredOn { get; private set; }

    public string Data { get; private set; }

    public EventState State { get; internal set; }

    public OutboxMessage(
        Guid transactionId,
        DateTime occurredOn,
        string data)
    {
        this.Id = Guid.NewGuid();
        this.TransactionId = transactionId;
        this.OccurredOn = occurredOn;
        this.Data = data;
        this.State = EventState.NotPublished;
    }
}

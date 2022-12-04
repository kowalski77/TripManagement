using MediatR;

namespace Arch.SharedKernel.DomainDriven;

public abstract class Entity
{
    private readonly List<INotification> domainEvents = new();

    public IEnumerable<INotification> DomainEvents => domainEvents;

    public bool SoftDeleted { get; protected set; }

    protected void AddDomainEvent(INotification eventItem) => domainEvents.Add(eventItem);

    public void ClearDomainEvents() => domainEvents.Clear();
}

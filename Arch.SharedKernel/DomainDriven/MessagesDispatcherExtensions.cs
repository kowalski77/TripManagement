using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Arch.SharedKernel.DomainDriven;

public static class MessagesDispatcherExtensions
{
    public static async Task PublishDomainEventsAsync(
        this IMediator mediator,
        DbContext context,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);

        List<EntityEntry<Entity>> domainEntities = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any()).ToList();

        List<INotification> domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        IEnumerable<Task> tasks = domainEvents
            .Select(async domainEvent => await mediator.Publish(domainEvent, cancellationToken).ConfigureAwait(false));

        await Task.WhenAll(tasks).ConfigureAwait(false);
    }
}

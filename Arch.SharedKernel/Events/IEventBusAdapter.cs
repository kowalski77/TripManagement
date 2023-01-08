namespace Arch.SharedKernel.Events;

public interface IEventBusAdapter
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default);

    Task PublishAsync(object message, Type messageType, CancellationToken cancellationToken = default);
}
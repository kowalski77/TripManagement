namespace Arch.SharedKernel.Events;

public interface IEventBusAdapter
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default);
}
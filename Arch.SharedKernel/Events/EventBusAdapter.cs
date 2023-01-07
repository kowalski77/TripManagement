using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Arch.SharedKernel.Events;

// This is an adapter class that is used to publish events to the event bus using a 3rd party library (Azure Service Bus, MassTransit,...) 
// in order to follow the guideline "only mock types that you own". 
public class EventBusAdapter : IEventBusAdapter
{
    private readonly ILogger<EventBusAdapter> logger;

    public EventBusAdapter(ILogger<EventBusAdapter> logger) => this.logger = logger;

    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(@event, new JsonSerializerOptions { WriteIndented = true });

        this.logger.LogPublishedEvent(json);

        return Task.CompletedTask;
    }
}

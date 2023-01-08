using Arch.SharedKernel.Events;
using FluentAssertions;

namespace TripManagement.IntegrationTests.Mocks;

public class EventBusAdapterSpy : IEventBusAdapter
{
    private readonly List<object> eventsPublished = new();

    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
    {
        this.eventsPublished.Add(@event);

        return Task.CompletedTask;
    }

    public Task PublishAsync(object message, Type messageType, CancellationToken cancellationToken = default)
    {
        this.eventsPublished.Add(message);

        return Task.CompletedTask;
    }

    public EventBusAdapterSpy Verify<TEvent>(TEvent @event)
    {
        this.eventsPublished.Should().ContainSingle(e => e.GetType() == typeof(TEvent) && e.Equals(@event));

        return this;
    }

    public EventBusAdapterSpy VerifyNoOtherEvents()
    {
        this.eventsPublished.Should().HaveCount(1);

        return this;
    }

    public EventBusAdapterSpy VerifyNoEvents()
    {
        this.eventsPublished.Should().BeEmpty();

        return this;
    }

    public EventBusAdapterSpy Verify<TEvent>(int count)
    {
        this.eventsPublished.Should().Contain(e => e.GetType() == typeof(TEvent)).And.HaveCount(count);

        return this;
    }

    public void Clear()
    {
        this.eventsPublished.Clear();
    }
}

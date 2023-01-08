using Arch.SharedKernel.Events;
using Arch.SharedKernel.Outbox;
using AutoFixture;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TripManagement.Application;
using TripManagement.Domain;
using TripManagement.Infrastructure.Persistence;
using TripManagement.IntegrationTests.Mocks;

namespace TripManagement.IntegrationTests;

public sealed class TestServicesFactory : IAsyncLifetime
{
    private IServiceScope serviceScope = default!;

    public TestServicesFactory() => RegisterServices();

    public IFixture Fixture { get; } = new Fixture()
        .Customize(new TestServicesCustomization());

    public IMediator Mediator => GetService<IMediator>();

    public T GetService<T>() where T : notnull => serviceScope.ServiceProvider.GetRequiredService<T>();

    public Mock<IGeocodeAdapter> GeocodeAdapterMock { get; } = new();

    public EventBusAdapterSpy EventBusAdapterSpy { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        TripManagementContext dbContext = GetService<TripManagementContext>();
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();

        OutboxContext outboxContext = GetService<OutboxContext>();
        await outboxContext.Database.EnsureDeletedAsync();
        await outboxContext.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync()
    {
        serviceScope.Dispose();
        return Task.CompletedTask;
    }

    private void RegisterServices()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.Testing.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        ServiceCollection services = new();
        services.AddLogging();
        services.AddApplicationServices();
        services.AddSingleton<IEventBusAdapter, EventBusAdapterSpy>();
        services.AddDomainServices(config);
        services.AddRepositories();
        services.AddSqlPersistence(config.GetConnectionString("IntegrationTestsConnection")!);
        services.AddScoped(_ => GeocodeAdapterMock.Object);

        IServiceProvider serviceProvider = services.BuildServiceProvider();
        serviceScope = serviceProvider.CreateScope();
        this.EventBusAdapterSpy = (EventBusAdapterSpy)serviceScope.ServiceProvider.GetRequiredService<IEventBusAdapter>();
    }
}

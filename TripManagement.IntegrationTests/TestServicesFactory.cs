using AutoFixture;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripManagement.Application;
using TripManagement.Domain;
using TripManagement.Domain.Common;
using TripManagement.Infrastructure.Persistence;

namespace TripManagement.IntegrationTests;

public sealed class TestServicesFactory : IAsyncLifetime
{
    private readonly IServiceProvider serviceProvider;

    public TestServicesFactory()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.Testing.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        ServiceCollection services = new();
        services.AddApplicationServices();
        services.AddDomainServices();
        services.AddRepositories();
        services.AddSqlPersistence(config.GetConnectionString("IntegrationTestsConnection")!);
        services.AddScoped<ICoordinatesAgent, CoordinatesAgentSpy>();

        this.serviceProvider = services.BuildServiceProvider(true);
    }

    public IFixture Fixture { get; } = new Fixture();

    public T GetService<T>() where T : notnull
    {
        using IServiceScope scope = this.serviceProvider.CreateScope();
        return scope.ServiceProvider.GetRequiredService<T>();
    }

    public async Task InitializeAsync()
    {
        using IServiceScope scope = this.serviceProvider.CreateScope();
        TripManagementContext dbContext = scope.ServiceProvider.GetRequiredService<TripManagementContext>();
        
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync()
    {
        switch (serviceProvider)
        {
            case IDisposable disposable:
                disposable.Dispose();
                break;
        }

        return Task.CompletedTask;
    }
}

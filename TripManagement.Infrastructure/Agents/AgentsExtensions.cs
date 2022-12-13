using Microsoft.Extensions.DependencyInjection;
using TripManagement.Domain;

namespace TripManagement.Infrastructure.Agents;

public static class AgentsExtensions
{
    public static void AddAgents(this IServiceCollection services)
    {
        services.AddScoped<IGeocodeAdapter, FakeGeocodeAdapter>();
    }
}

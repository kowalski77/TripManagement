using Microsoft.Extensions.DependencyInjection;
using TripManagement.Domain.Common;

namespace TripManagement.Infrastructure.Agents;

public static class AgentsExtensions
{
    public static void AddAgents(this IServiceCollection services)
    {
        services.AddScoped<ICoordinatesAgent, FakeCoordinatesAgent>();
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripManagement.Infrastructure.Agents;
using TripManagement.Infrastructure.Persistence;

namespace TripManagement.Infrastructure;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAgents();
        services.AddRepositories();
        services.AddSqlPersistence(configuration.GetConnectionString("DefaultConnection")!);
    }
}

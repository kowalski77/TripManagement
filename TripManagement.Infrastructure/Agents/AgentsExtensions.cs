using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripManagement.Domain;

namespace TripManagement.Infrastructure.Agents;

public static class AgentsExtensions
{
    public static void AddAgents(this IServiceCollection services, IConfiguration configuration)
    {
        GeocodeApiOptions geocodeOptions = configuration.GetSection(nameof(GeocodeApiOptions)).Get<GeocodeApiOptions>()!;

        services.AddHttpClient<IGeocodeAdapter, GeocodeApiAdapter>(client => client.BaseAddress = new Uri(geocodeOptions.BaseUrl));
    }
}

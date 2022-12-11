using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripManagement.Domain.LocationsAggregate;
using TripManagement.Domain.TripsAggregate;

namespace TripManagement.Domain;

public static class DomainExtensions
{
    public static void AddDomainServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<LocationsService>();
        services.AddOptions<TripOptions>().Bind(configuration.GetSection(nameof(TripOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}

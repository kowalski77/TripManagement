using Microsoft.Extensions.DependencyInjection;
using TripManagement.Domain.LocationsAggregate;

namespace TripManagement.Domain;

public static class DomainExtensions
{
    public static void AddDomainServices(this IServiceCollection services) => 
        services
        .AddScoped<LocationsService>();
}

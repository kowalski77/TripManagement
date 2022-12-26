using Arch.SharedKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripManagement.Domain.TripsAggregate;
using TripManagement.Domain.Types.Locations;

namespace TripManagement.Domain;

public static class DomainExtensions
{
    public static void AddDomainServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<LocationsService>();
        services.AddOptions<TripOptions>().Bind(configuration.NonNull().GetSection(nameof(TripOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}

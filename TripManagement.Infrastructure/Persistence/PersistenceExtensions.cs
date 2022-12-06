using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TripManagement.Domain.TripsAggregate;

namespace TripManagement.Infrastructure.Persistence;

internal static class PersistenceExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITripRepository, TripsRepository>();
    }

    public static void AddSqlPersistence(this IServiceCollection services, string connectionString)
    {
        connectionString.NonNullOrEmpty();

        services.AddDbContext<TripManagementContext>(options => 
            options.UseSqlServer(connectionString, sqlOptions => 
                sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null)));
        
        services.AddScoped<IDbContext, TripManagementContext>();
    }
}


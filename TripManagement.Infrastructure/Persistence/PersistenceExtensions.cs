using System.Reflection;
using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using Arch.SharedKernel.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TripManagement.Domain.TripsAggregate;

namespace TripManagement.Infrastructure.Persistence;

public static class PersistenceExtensions
{
    public static void AddRepositories(this IServiceCollection services) => services.AddScoped<ITripsRepository, TripsRepository>();

    public static void AddSqlPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TripManagementContext>(options =>
            options.UseSqlServer(connectionString.NonNullOrEmpty(), sqlOptions =>
                sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null)));

        services.AddDbContext<OutboxContext>(options =>
            options.UseSqlServer(connectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(TripManagementContext).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                }));

        services.AddScoped<IDbContext, TripManagementContext>();
    }
}

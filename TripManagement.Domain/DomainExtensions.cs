﻿using Microsoft.Extensions.DependencyInjection;
using TripManagement.Domain.TripsAggregate.Services;

namespace TripManagement.Domain;

public static class DomainExtensions
{
    public static void AddDomainServices(this IServiceCollection services) => 
        services
        .AddScoped<TripService>()
        .AddScoped<LocationFactory>();
}

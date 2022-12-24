using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using TripManagement.Domain;

namespace TripManagement.Infrastructure.Agents;

public static class AgentsExtensions
{
    public static void AddAgents(this IServiceCollection services, IConfiguration configuration)
    {
        GeocodeApiOptions geocodeOptions = configuration.GetSection(nameof(GeocodeApiOptions)).Get<GeocodeApiOptions>()!;

        services.AddOptions<GeocodeApiOptions>()
            .Bind(configuration.GetSection(nameof(GeocodeApiOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddHttpClient<IGeocodeAdapter, GeocodeApiAdapter>()
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() => 
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
}

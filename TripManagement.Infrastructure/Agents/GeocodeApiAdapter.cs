using System.Net.Http.Json;
using Arch.SharedKernel.Results;
using Geocoding.Contracts;
using Microsoft.Extensions.Options;
using TripManagement.Domain;
using TripManagement.Domain.Types.Coordinates;
using Location = TripManagement.Domain.Types.Locations.Location;

namespace TripManagement.Infrastructure.Agents;

public sealed class GeocodeApiAdapter : IGeocodeAdapter
{
    private readonly HttpClient httpClient;
    private readonly GeocodeApiOptions options;

    public GeocodeApiAdapter(HttpClient httpClient, IOptions<GeocodeApiOptions> options)
    {
        this.httpClient = httpClient;
        this.options = options.Value;
        this.httpClient.BaseAddress = new Uri(this.options.BaseUrl);
    }

    public async Task<Result<Location>> GetLocationByCoordinatesAsync(Coordinate coordinates, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.GetAsync($"{this.options.LocationByCoordinatesEndpoint}?latitude={coordinates.Latitude}&longitude={coordinates.Longitude}", cancellationToken);

        return response.IsSuccessStatusCode ?
            (await response.Content.ReadFromJsonAsync<Place>(cancellationToken: cancellationToken))!.ToLocation() :
            new ErrorResult("Unable to get location by coordinates", response.ReasonPhrase);
    }
}

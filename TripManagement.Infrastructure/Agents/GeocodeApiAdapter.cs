using System.Net.Http.Json;
using Arch.SharedKernel.Results;
using Geocoding.Contracts;
using TripManagement.Domain;
using TripManagement.Domain.Types.Coordinates;
using Location = TripManagement.Domain.Types.Locations.Location;

namespace TripManagement.Infrastructure.Agents;

public sealed class GeocodeApiAdapter : IGeocodeAdapter
{
    private const string locationsCoordinatesEndpoint = "/locations/coordinates";
    private readonly HttpClient httpClient;

    public GeocodeApiAdapter(HttpClient httpClient) => this.httpClient = httpClient;

    public async Task<Result<Location>> GetLocationByCoordinatesAsync(Coordinate coordinates, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.GetAsync($"{locationsCoordinatesEndpoint}?latitude={coordinates.Latitude}&longitude={coordinates.Longitude}", cancellationToken);
        
        return response.IsSuccessStatusCode ?
            (await response.Content.ReadFromJsonAsync<Place>(cancellationToken: cancellationToken))!.ToLocation() :
            new ErrorResult("Unable to get location by coordinates", response.ReasonPhrase);
    }
}

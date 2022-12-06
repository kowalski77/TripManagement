using Arch.SharedKernel;
using TripManagement.Domain.Common;

namespace TripManagement.Infrastructure.Agents;

public sealed class FakeCoordinatesAgent : ICoordinatesAgent
{
    // TODO: Fake Agent Service, dummy implementations, replace with Google API; since 3rd party agent, handle exceptions and timeouts
    public Task<Maybe<string>> GetCityByCoordinatesAsync(Coordinates coordinates, CancellationToken cancellationToken = default) => 
        coordinates.NonNull().Latitude > 0 ? 
        Task.FromResult((Maybe<string>)"Barcelona") : 
        Task.FromResult((Maybe<string>)"Sabadell");

    public Task<Maybe<string>> GetLocationByCoordinatesAsync(Coordinates coordinates, CancellationToken cancellationToken = default) =>
        coordinates.NonNull().Latitude > 0 ? 
        Task.FromResult((Maybe<string>)"Barcelona") : 
        Task.FromResult((Maybe<string>)"Sabadell");

    public Task<int> GetDistanceInKmBetweenCoordinatesAsync(Coordinates origin, Coordinates destination, CancellationToken cancellationToken = default) =>
        Task.FromResult(5);
}

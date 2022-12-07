using Arch.SharedKernel;
using TripManagement.Domain.Common;

namespace TripManagement.IntegrationTests;

internal class CoordinatesAgentSpy : ICoordinatesAgent
{
    public Task<Maybe<string>> GetCityByCoordinatesAsync(Coordinates coordinates, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    
    public Task<int> GetDistanceInKmBetweenCoordinatesAsync(Coordinates origin, Coordinates destination, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    
    public Task<Maybe<string>> GetLocationByCoordinatesAsync(Coordinates coordinates, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}

using Arch.SharedKernel;

namespace TripManagement.Domain.Common;

public interface ICoordinatesAgent
{
    Task<Maybe<string>> GetCityByCoordinatesAsync(Coordinates coordinates, CancellationToken cancellationToken = default);

    Task<Maybe<string>> GetLocationByCoordinatesAsync(Coordinates coordinates, CancellationToken cancellationToken = default);
}

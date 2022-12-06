using Arch.SharedKernel;
using Arch.SharedKernel.Results;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.TripsAggregate.Services;

public sealed class TripsService
{
    private readonly LocationsService locationsService;
    private readonly ICoordinatesAgent coordinatesAgent;

    public TripsService(LocationsService locationsService, ICoordinatesAgent coordinatesAgent)
    {
        this.locationsService = locationsService;
        this.coordinatesAgent = coordinatesAgent;
    }

    public async Task<Result<Trip>> CreateDraftTripAsync(
        UserId userId, DateTime pickUp,
        Coordinates origin, Coordinates destination,
        CancellationToken cancellationToken = default) => await Result.Init
            .OnSuccess(async () => await this.ValidateDistanceBetweenCoordinatesAsync(origin.NonNull(), destination.NonNull(), cancellationToken))
            .OnSuccess(async () => await this.locationsService.CreateTripLocationsAsync(origin, destination, cancellationToken))
            .OnSuccess(locations => new Trip(Guid.NewGuid(), userId.NonNull(), pickUp, locations.Item1, locations.Item2))
            .OnSuccess(trip => trip.CalculateTripCredits());

    private async Task<Result> ValidateDistanceBetweenCoordinatesAsync(
        Coordinates origin, Coordinates destination, CancellationToken cancellationToken)
    {
        var distance = await this.coordinatesAgent.GetDistanceInKmBetweenCoordinatesAsync(origin, destination, cancellationToken);
        return Trip.ValidateDistance(distance);
    }
}

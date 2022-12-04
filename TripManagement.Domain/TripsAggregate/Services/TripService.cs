using Arch.SharedKernel;
using Arch.SharedKernel.Results;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.TripsAggregate.Services;

public sealed class TripService
{
    private readonly LocationFactory locationFactory;
    private readonly ICoordinatesAgent coordinatesAgent;

    public TripService(LocationFactory locationFactory, ICoordinatesAgent coordinatesAgent)
    {
        this.locationFactory = locationFactory.NonNull();
        this.coordinatesAgent = coordinatesAgent;
    }

    public async Task<Result<Trip>> CreateDraftTripAsync(
        UserId userId, DateTime pickUp,
        Coordinates origin, Coordinates destination,
        CancellationToken cancellationToken = default) => await Result.Init
            .OnSuccess(async () => await ValidateDistanceBetweenCoordinatesAsync(origin.NonNull(), destination.NonNull(), cancellationToken))
            .OnSuccess(async () => await this.CreateLocationsAsync(origin, destination, cancellationToken))
            .OnSuccess(locations => new Trip(Guid.NewGuid(), userId.NonNull(), pickUp, locations.Item1, locations.Item2))
            .OnSuccess(trip => trip.CalculateTripCredits());

    private async Task<Result> ValidateDistanceBetweenCoordinatesAsync(
        Coordinates origin, Coordinates destination, CancellationToken cancellationToken) =>
        Trip.ValidateDistance(await this.coordinatesAgent.GetDistanceInKmBetweenCoordinatesAsync(origin, destination, cancellationToken));

    private async Task<Result<(Location, Location)>> CreateLocationsAsync(Coordinates origin, Coordinates destination, CancellationToken cancellationToken)
    {
        Result<Location> originLocation = await locationFactory.CreateAsync(origin, cancellationToken);
        if (originLocation.Failure)
        {
            return originLocation.Error!;
        }

        Result<Location> destinationLocation = await locationFactory.CreateAsync(destination, cancellationToken);
        return destinationLocation.Failure ? destinationLocation.Error : (originLocation.Value, destinationLocation.Value);
    }
}

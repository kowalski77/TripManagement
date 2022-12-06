using Arch.SharedKernel.Mediator;
using Arch.SharedKernel.Results;
using TripManagement.Contracts.Models;
using TripManagement.Domain.Common;
using TripManagement.Domain.TripsAggregate;
using TripManagement.Domain.TripsAggregate.Services;

namespace TripManagement.Application.Trips.CreateDraft;

public sealed record Request(CreateDraftRequest CreateDraft) : ICommand<Result<CreateDraftResponse>>;

public sealed class Handler : ICommandHandler<Request, Result<CreateDraftResponse>>
{
    private readonly LocationsService locationsService;
    private readonly ITripsRepository tripRepository;
    private readonly ICoordinatesAgent coordinatesAgent;

    public Handler(LocationsService locationsService, ITripsRepository tripRepository, ICoordinatesAgent coordinatesAgent)
    {
        this.locationsService = locationsService;
        this.tripRepository = tripRepository;
        this.coordinatesAgent = coordinatesAgent;
    }

    public async Task<Result<CreateDraftResponse>> Handle(Request request, CancellationToken cancellationToken)
    {
        Result<Coordinates> originCoordinates = Coordinates.CreateInstance(request.CreateDraft.OriginLatitude, request.CreateDraft.OriginLongitude);
        Result<Coordinates> destinationCoordinates = Coordinates.CreateInstance(request.CreateDraft.DestinationLatitude, request.CreateDraft.DestinationLongitude);
        Result<UserId> userId = UserId.CreateInstance(request.CreateDraft.UserId);

        Result<CreateDraftResponse> resultModel = await Result.Init
            .Validate(originCoordinates, destinationCoordinates, userId)
            .OnSuccess(async () => await CreateTripAsync(userId.Value, request.CreateDraft.PickUp, originCoordinates.Value, destinationCoordinates.Value, cancellationToken))
            .OnSuccess(trip => new CreateDraftResponse(trip.Id));

        return resultModel;
    }

    private async Task<Result<Trip>> CreateTripAsync(UserId userId, DateTime pickUp, Coordinates origin, Coordinates destination, CancellationToken cancellationToken)
    {
        var distanceInKm = await coordinatesAgent.GetDistanceInKmBetweenCoordinatesAsync(origin, destination, cancellationToken);
        Result dinstanceValidationResult = Trip.ValidateDistance(distanceInKm);
        if (dinstanceValidationResult.Failure)
        {
            return dinstanceValidationResult.Error!;
        }

        Result<(Location originLocation, Location destinationLocation)> locations = await locationsService.CreateTripLocationsAsync(origin, destination, cancellationToken);
        if (locations.Failure)
        {
            return locations.Error!;
        }

        Trip trip = new(Guid.NewGuid(), userId, pickUp, locations.Value.originLocation, locations.Value.destinationLocation);

        tripRepository.Add(trip);
        await tripRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return trip;
    }
}

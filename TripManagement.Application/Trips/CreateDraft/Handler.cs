using Arch.SharedKernel.Results;
using MediatR;
using TripManagement.Contracts.Models;
using TripManagement.Domain.Common;
using TripManagement.Domain.TripsAggregate;
using TripManagement.Domain.TripsAggregate.Services;

namespace TripManagement.Application.Trips.CreateDraft;

public sealed record Request(CreateDraftRequest CreateDraft) : IRequest<Result<CreateDraftResponse>>;

public sealed class Handler : IRequestHandler<Request, Result<CreateDraftResponse>>
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
        var originCoordinates = Coordinates.CreateInstance(request.CreateDraft.Origin.Latitude, request.CreateDraft.Origin.Longitude);
        var destinationCoordinates = Coordinates.CreateInstance(request.CreateDraft.Destination.Latitude, request.CreateDraft.Destination.Longitude);
        var userId = UserId.CreateInstance(request.CreateDraft.UserId);

        return await Result.Init
            .Validate(originCoordinates, destinationCoordinates, userId)
            .OnSuccess(async () => await CreateTripAsync(userId.Value, request.CreateDraft.PickUp, originCoordinates.Value, destinationCoordinates.Value, cancellationToken))
            .OnSuccess(trip => new CreateDraftResponse(trip.Id));
    }

    private async Task<Result<Trip>> CreateTripAsync(UserId userId, DateTime pickUp, Coordinates origin, Coordinates destination, CancellationToken cancellationToken)
    {
        var distanceInKmResult = await coordinatesAgent.GetDistanceInKmBetweenCoordinatesAsync(origin, destination, cancellationToken);

        return await Trip.ValidateDistance(distanceInKmResult)
            .OnSuccess(async () => await locationsService.CreateTripLocationsAsync(origin, destination, cancellationToken))
            .OnSuccess(async locations =>
            {
                Trip trip = new(Guid.NewGuid(), userId, pickUp, locations.origin, locations.destination);
                tripRepository.Add(trip);
                await tripRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return trip;
            });
    }
}

using Arch.SharedKernel.Mediator;
using Arch.SharedKernel.Results;
using TripManagement.Contracts.Models;
using TripManagement.Domain.Common;
using TripManagement.Domain.TripsAggregate;
using TripManagement.Domain.TripsAggregate.Services;

namespace TripManagement.Application.Trips.CreateDraft;

public sealed record Request(CreateDraftRequest CreateDraft) : ICommand<Result<CreateDraftResponse>>;

public sealed class CreateDraftTripHandler : ICommandHandler<Request, Result<CreateDraftResponse>>
{
    private readonly TripsService tripService;
    private readonly ITripRepository tripRepository;

    public CreateDraftTripHandler(TripsService tripService, ITripRepository tripRepository)
    {
        this.tripService = tripService;
        this.tripRepository = tripRepository;
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
        Result<Trip> result = await tripService.CreateDraftTripAsync(userId, pickUp, origin, destination, cancellationToken);
        if (result.Failure)
        {
            return result.Error!;
        }

        Trip trip = tripRepository.Add(result.Value);
        await tripRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return trip;
    }
}

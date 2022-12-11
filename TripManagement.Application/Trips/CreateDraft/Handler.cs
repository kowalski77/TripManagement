using Arch.SharedKernel.Results;
using MediatR;
using Microsoft.Extensions.Options;
using TripManagement.Contracts.Models;
using TripManagement.Domain.Common;
using TripManagement.Domain.LocationsAggregate;
using TripManagement.Domain.TripsAggregate;

namespace TripManagement.Application.Trips.CreateDraft;

public sealed record Request(CreateDraftRequest CreateDraft) : IRequest<Result<CreateDraftResponse>>;

public sealed class Handler : IRequestHandler<Request, Result<CreateDraftResponse>>
{
    private readonly TripOptions tripOptions;
    private readonly LocationsService locationsService;
    private readonly ITripsRepository tripRepository;

    public Handler(IOptions<TripOptions> tripOptions, LocationsService locationsService, ITripsRepository tripRepository)
    {
        this.tripOptions = tripOptions.Value;
        this.locationsService = locationsService;
        this.tripRepository = tripRepository;
    }

    public async Task<Result<CreateDraftResponse>> Handle(Request request, CancellationToken cancellationToken)
    {
        var originCoordinates = Coordinates.CreateInstance(request.CreateDraft.OriginLatitude, request.CreateDraft.OriginLongitude);
        var destinationCoordinates = Coordinates.CreateInstance(request.CreateDraft.DestinationLatitude, request.CreateDraft.DestinationLongitude);
        var userId = UserId.CreateInstance(request.CreateDraft.UserId);

        return (await Result.Init
            .Validate(originCoordinates, destinationCoordinates, userId)
            .Act(async () => await CreateTripAsync(userId.Value, request.CreateDraft.PickUp, originCoordinates.Value, destinationCoordinates.Value, cancellationToken)))
            .Map(x=> new CreateDraftResponse(x.Id));
    }

    private async Task<Result<Trip>> CreateTripAsync(UserId userId, DateTime pickUp, Coordinates origin, Coordinates destination, CancellationToken cancellationToken) =>
        await locationsService.CreateTripLocationsAsync(origin, destination, cancellationToken)
            .Act(locations => Trip.CreateDraft(Guid.NewGuid(), userId, pickUp, locations.origin, locations.destination, this.tripOptions))
            .Act(async trip =>
            {
                tripRepository.Add(trip);
                await tripRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return trip;
            });
}

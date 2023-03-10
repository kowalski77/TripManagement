using Arch.SharedKernel.Results;
using MediatR;
using Microsoft.Extensions.Options;
using TripManagement.Contracts;
using TripManagement.Domain.TripsAggregate;
using TripManagement.Domain.Types.Coordinates;
using TripManagement.Domain.Types.Locations;

namespace TripManagement.Application.Trips.DraftTrip;

public sealed record Request(CreateDraftTripRequest CreateDraft) : IRequest<Result<CreateDraftTripResponse>>;

public sealed class Handler : IRequestHandler<Request, Result<CreateDraftTripResponse>>
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

    public async Task<Result<CreateDraftTripResponse>> Handle(Request request, CancellationToken cancellationToken)
    {
        Result<Coordinate> originCoordinates = Coordinate.CreateInstance(request.CreateDraft.OriginLatitude, request.CreateDraft.OriginLongitude);
        Result<Coordinate> destinationCoordinates = Coordinate.CreateInstance(request.CreateDraft.DestinationLatitude, request.CreateDraft.DestinationLongitude);
        Result<UserId> userId = UserId.CreateInstance(request.CreateDraft.UserId);

        return (await Result.Init
            .Validate(originCoordinates, destinationCoordinates, userId)
            .OnSucess(async () => await CreateTripAsync(userId.Value, request.CreateDraft.PickUp, originCoordinates.Value, destinationCoordinates.Value, cancellationToken)))
            .OnSuccess(trip => new CreateDraftTripResponse(trip.Id));
    }

    private async Task<Result<Trip>> CreateTripAsync(UserId userId, DateTime pickUp, Coordinate origin, Coordinate destination, CancellationToken cancellationToken) =>
        await locationsService.CreateTripLocationsAsync(origin, destination, cancellationToken)
            .OnSuccess(locations => Draft.Create(Guid.NewGuid(), userId, pickUp, locations.origin, locations.destination, tripOptions))
            .OnSuccess(async trip =>
            {
                tripRepository.Add(trip);
                await tripRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return trip;
            });
}

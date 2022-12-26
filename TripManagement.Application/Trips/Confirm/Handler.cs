using Arch.SharedKernel;
using Arch.SharedKernel.Results;
using MediatR;
using TripManagement.Contracts;
using TripManagement.Domain.TripsAggregate;

namespace TripManagement.Application.Trips.Confirm;

public sealed record Request(ConfirmTripRequest Confirm) : IRequest<Result>;

public sealed class Handler : IRequestHandler<Request, Result>
{
    private readonly ITripsRepository tripRepository;

    public Handler(ITripsRepository tripRepository)
    {
        this.tripRepository = tripRepository;
    }

    public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
    {
        Maybe<Trip> maybeTrip = await this.tripRepository.GetAsync(request.Confirm.TripId, cancellationToken);

        var res = maybeTrip.Match(
            (a) =>Result.Ok(),
            () => Result.Fail(new ErrorResult("", "")));

        return res;
    }

    //private async Task<Result> ConfirmTripAsync(Trip trip) 
    //{
    //    var result = trip.CanConfirm();
    //    if (result.Success)
    //    {
    //        var confirmedTrip = trip.Confirm();
    //        await this.tripRepository.
    //        return Result.Ok();
    //    }
    //}
}
using Arch.SharedKernel.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripManagement.Contracts;

namespace TripManagement.Application.Trips.DraftTrip;

public static class Endpoint
{
    public static RouteHandlerBuilder MapCreateDraft(this RouteGroupBuilder groupBuilder) =>
        groupBuilder.MapPost("draft", CreateDraft)
            .WithSummary("Creates a draft trip")
            .WithName("CreateDraftTrip")
            .Produces<CreateDraftTripResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

    private static async Task<IResult> CreateDraft(IMediator mediator, CreateDraftTripRequest request, CancellationToken cancellationToken)
    {
        Result<CreateDraftTripResponse> result = await mediator.Send(new Request(request), cancellationToken);
        return result.Success ?
            Results.Ok(result.Value) :
            Results.BadRequest(result.Error);
        // TODO: Improve this mechanism with a custom middleware that will handle the result and return the appropriate status code + Problem Details
    }
}

using TripManagement.Application.Trips.DraftTrip;

namespace TripManagement.API.Api;

internal static class TripsApi
{
    public static RouteGroupBuilder MapTripsEndpoints(this RouteGroupBuilder group)
    {
        group.MapCreateDraft();
        return group
            .WithOpenApi()
            .WithTags("Trips");
    }
}

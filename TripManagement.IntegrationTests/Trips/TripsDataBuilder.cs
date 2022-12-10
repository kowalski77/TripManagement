using AutoFixture;
using TripManagement.Application.Trips.CreateDraft;
using TripManagement.Contracts.Models;

namespace TripManagement.IntegrationTests.Trips;

public static class TripsDataBuilder
{
    public static Request CreateDraftTripRequest(this IFixture fixture)
    {
        CreateDraftRequest createDraftRequest = fixture.Build<CreateDraftRequest>()
            .With(x => x.OriginLatitude, 41.54)
            .With(x => x.OriginLongitude, 2.10)
            .With(x => x.DestinationLatitude, 41.38)
            .With(x => x.DestinationLongitude, 2.17)
            .Create();

        return fixture.Build<Request>().With(x => x.CreateDraft, createDraftRequest).Create();
    }
}

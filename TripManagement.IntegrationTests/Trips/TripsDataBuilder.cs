using AutoFixture;
using TripManagement.Application.Trips.DraftTrip;
using TripManagement.Contracts;
using TripManagement.Domain.TripsAggregate;
using TripManagement.Domain.Types.Coordinates;
using TripManagement.Domain.Types.Locations;

namespace TripManagement.IntegrationTests.Trips;

public static class TripsDataBuilder
{
    private const double CityOneLatitude = 41.54;
    private const double CityOneLongitude = 2.10;
    private const double CityTwoLatitude = 41.38;
    private const double CityTwoLongitude = 2.17;

    public static Request CreateDraftTripRequest(this IFixture fixture)
    {
        CreateDraftTripRequest createDraftRequest = fixture.Build<CreateDraftTripRequest>()
            .With(x => x.OriginLatitude, CityOneLatitude)
            .With(x => x.OriginLongitude, CityOneLongitude)
            .With(x => x.DestinationLatitude, CityTwoLatitude)
            .With(x => x.DestinationLongitude, CityTwoLongitude)
            .Create();

        return fixture.Build<Request>().With(x => x.CreateDraft, createDraftRequest).Create();
    }

    public static Trip CreateDrafTrip(this IFixture fixture) =>
        Draft.Create(
            Guid.NewGuid(),
            UserId.CreateInstance(Guid.NewGuid()).Value,
            fixture.Create<DateTime>(),
            fixture.CreateOriginLocation(),
            fixture.CreateDestinationLocation(),
            fixture.CreateValidOptions()).Value;

    public static Location CreateOriginLocation(this IFixture fixture)
    {
        fixture.Customize<City>(x => x.With(y => y.Value, "Sabadell"));

        return Location.Create(
            Guid.NewGuid(),
            fixture.Create<Address>(),
            fixture.Create<City>(),
            fixture.Create<PlaceId>(),
            Coordinate.CreateInstance(CityOneLatitude, CityOneLongitude).Value);
    }

    public static Location CreateDestinationLocation(this IFixture fixture)
    {
        fixture.Customize<City>(x => x.With(y => y.Value, "Barcelona"));
        
        return Location.Create(
            Guid.NewGuid(),
            fixture.Create<Address>(),
            fixture.Create<City>(),
            fixture.Create<PlaceId>(),
            Coordinate.CreateInstance(CityTwoLatitude, CityTwoLongitude).Value);
    }

    private static TripOptions CreateValidOptions(this IFixture fixture) =>
        fixture.Build<TripOptions>()
            .With(x => x.MaxDistanceBetweenLocations, 100)
            .With(x => x.MinDistanceBetweenLocations, 1)
            .With(x => x.AllowedCities, new[] { "Barcelona", "Sabadell" })
            .Create();
}

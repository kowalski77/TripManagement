using AutoFixture;
using TripManagement.Domain.TripsAggregate;
using TripManagement.Domain.Types.Coordinates;
using TripManagement.Domain.Types.Locations;

namespace TripManagement.UnitTests.Trips;

public static class TripsDataBuilder
{
    private const double CityOneLatitude = 41.54;
    private const double CityOneLongitude = 2.10;
    private const double CityTwoLatitude = 41.38;
    private const double CityTwoLongitude = 2.17;

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

    public static Trip CreateDrafTrip(this IFixture fixture) => 
        Draft.Create(
            Guid.NewGuid(),
            UserId.CreateInstance(Guid.NewGuid()).Value,
            fixture.Create<DateTime>(),
            fixture.CreateOriginLocation(),
            fixture.CreateDestinationLocation(),
            fixture.CreateValidOptions()).Value;

    private static TripOptions CreateValidOptions(this IFixture fixture) =>
        fixture.Build<TripOptions>()
            .With(x => x.MaxDistanceBetweenLocations, 100)
            .With(x => x.MinDistanceBetweenLocations, 1)
            .With(x => x.AllowedCities, new[] { "Barcelona", "Sabadell" })
            .Create();
}

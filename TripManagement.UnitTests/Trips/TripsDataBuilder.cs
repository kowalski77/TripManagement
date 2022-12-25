using AutoFixture;
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
}

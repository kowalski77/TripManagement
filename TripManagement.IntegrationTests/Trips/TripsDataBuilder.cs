﻿using AutoFixture;
using TripManagement.Application.Trips.CreateDraft;
using TripManagement.Contracts;
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

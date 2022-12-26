using Arch.SharedKernel.Results;
using AutoFixture;
using FluentAssertions;
using TripManagement.Domain.TripsAggregate;
using TripManagement.Domain.TripsAggregate.DraftTrip;
using TripManagement.Domain.Types.Locations;

namespace TripManagement.UnitTests.Trips;

public class DraftTripTests
{
    private readonly IFixture fixture = new Fixture();

    [Fact]
    public void Draft_trip_is_created()
    {
        // Arrange
      TripOptions tripOptions =  fixture.Build<TripOptions>()
            .With(x => x.MaxDistanceBetweenLocations, 100)
            .With(x => x.MinDistanceBetweenLocations, 1)
            .With(x => x.AllowedCities, new[] { "Barcelona", "Sabadell" })
            .Create();

        // Act
        Result<Trip> result = Draft.Create(
            Guid.NewGuid(),
            UserId.CreateInstance(Guid.NewGuid()).Value,
            fixture.Create<DateTime>(),
            fixture.CreateOriginLocation(),
            fixture.CreateDestinationLocation(),
            tripOptions);

        // Assert
        result.Success.Should().BeTrue();
    }

    [Fact]
    public void Draft_with_city_not_allowed_is_not_created()
    {
        // Arrange
        TripOptions tripOptions = fixture.Build<TripOptions>()
            .With(x => x.MaxDistanceBetweenLocations, 100)
            .With(x => x.MinDistanceBetweenLocations, 1)
            .With(x => x.AllowedCities, new[] { "Terrassa" })
            .Create();

        // Act
        Result<Trip> result = Draft.Create(
            Guid.NewGuid(),
            UserId.CreateInstance(Guid.NewGuid()).Value,
            DateTime.Now,
            fixture.CreateOriginLocation(),
            fixture.CreateDestinationLocation(),
            tripOptions);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void Draft_with_distance_between_locations_greater_than_max_is_not_created()
    {
        // Arrange
        TripOptions tripOptions = fixture.Build<TripOptions>()
            .With(x => x.MaxDistanceBetweenLocations, 1)
            .With(x => x.MinDistanceBetweenLocations, 1)
            .With(x => x.AllowedCities, new[] { "Barcelona", "Sabadell" })
            .Create();

        // Act
        Result<Trip> result = Draft.Create(
            Guid.NewGuid(),
            UserId.CreateInstance(Guid.NewGuid()).Value,
            DateTime.Now,
            fixture.CreateOriginLocation(),
            fixture.CreateDestinationLocation(),
            tripOptions);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void Draft_with_distance_between_locations_lower_than_min_is_not_created()
    {
        // Arrange
        TripOptions tripOptions = fixture.Build<TripOptions>()
            .With(x => x.MaxDistanceBetweenLocations, 100)
            .With(x => x.MinDistanceBetweenLocations, 100)
            .With(x => x.AllowedCities, new[] { "Barcelona", "Sabadell" })
            .Create();

        // Act
        Result<Trip> result = Draft.Create(
            Guid.NewGuid(),
            UserId.CreateInstance(Guid.NewGuid()).Value,
            DateTime.Now,
            fixture.CreateOriginLocation(),
            fixture.CreateDestinationLocation(),
            tripOptions);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void Draft_with_same_origin_and_destination_is_not_created()
    {
        // Arrange
        TripOptions tripOptions = fixture.Build<TripOptions>()
            .With(x => x.MaxDistanceBetweenLocations, 100)
            .With(x => x.MinDistanceBetweenLocations, 1)
            .With(x => x.AllowedCities, new[] { "Barcelona", "Sabadell" })
            .Create();

        Location location = fixture.CreateOriginLocation();

        // Act
        Result<Trip> result = Draft.Create(
            Guid.NewGuid(),
            UserId.CreateInstance(Guid.NewGuid()).Value,
            DateTime.Now,
            location,
            location,
            tripOptions);

        // Assert
        result.Success.Should().BeFalse();
    }
}

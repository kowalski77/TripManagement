using Arch.SharedKernel.Results;
using AutoFixture;
using FluentAssertions;
using TripManagement.Domain.TripsAggregate;

namespace TripManagement.UnitTests.Trips;

public class ConfirmTripTests
{
    private readonly IFixture fixture = new Fixture();

    [Fact]
    public void Trip_can_confirm()
    {
        // Arrange
        Trip trip = this.fixture.CreateDrafTrip();

        // Act
        Result canConfirm = trip.CanConfirm();

        // Assert
        canConfirm.Success.Should().BeTrue();
    }

    [Fact]
    public void Trip_can_not_confirm()
    {
        // Arrange
        Trip trip = this.fixture.CreateDrafTrip();
        trip.Confirm();

        // Act
        Result canConfirm = trip.CanConfirm();

        // Assert
        canConfirm.Success.Should().BeFalse();
    }

    [Fact]
    public void Draft_trip_is_confirmed()
    {
        // Arrange
        Trip trip = this.fixture.CreateDrafTrip();

        // Act
        trip.Confirm();

        // Assert
        trip.TripStatus.Should().Be(TripStatus.Confirmed);
    }

    [Fact]
    public void Confirmed_trip_cannot_be_confirmed_again()
    {
        // Arrange
        Trip trip = this.fixture.CreateDrafTrip();
        trip.Confirm();

        // Act
        Action action = () => trip.Confirm();

        // Assert
        action.Should().Throw<TripConfirmationException>();
    }
}

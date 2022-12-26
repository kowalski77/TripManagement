using Arch.SharedKernel.Results;
using FluentAssertions;
using TripManagement.Application.Trips.Confirm;
using TripManagement.Contracts;
using TripManagement.Domain.TripsAggregate;
using TripManagement.Infrastructure.Persistence;

namespace TripManagement.IntegrationTests.Trips;

[Collection(IntegrationTestsConstants.IntegrationTestsCollection)]
public class ConfirmTripTests
{
    private readonly TestServicesFactory factory;

    public ConfirmTripTests(TestServicesFactory factory)
    {
        this.factory = factory;
        this.factory.DeleteAllTrips();
    }

    [Fact]
    public async Task Trip_is_confirmed()
    {
        // Arrange
        Trip trip = this.factory.Fixture.CreateDrafTrip();
        
        TripManagementContext context = this.factory.GetService<TripManagementContext>();
        context.Add(trip);
        await context.SaveChangesAsync();

        Request request = new(new ConfirmTripRequest(trip.Id));

        // Act
        Result response = await factory.Mediator.Send(request);

        // Assert
        response.Success.Should().BeTrue();
    }

    [Fact]
    public async Task Trip_does_not_exists()
    {
        // Arrange
        Request request = new(new ConfirmTripRequest(Guid.NewGuid()));

        // Act
        Result response = await factory.Mediator.Send(request);

        // Assert
        response.Success.Should().BeFalse();
    }
}

using Arch.SharedKernel.Results;
using FluentAssertions;
using Moq;
using TripManagement.Application.Trips.CreateDraft;
using TripManagement.Contracts.Models;
using TripManagement.Domain.Common;

namespace TripManagement.IntegrationTests.Trips;

[Collection(IntegrationTestsConstants.IntegrationTestsCollection)]
public class CreateDraftTripTests
{
    private readonly TestServicesFactory factory;

    public CreateDraftTripTests(TestServicesFactory factory)
    {
        this.factory = factory;
        this.factory.DeleteAllTrips();
    }

    [Fact]
    public async Task Draft_trip_is_created()
    {
        // Arrange
        factory.GeocodeAdapterMock.SetupSequence(x => x.GetLocationByCoordinatesAsync(It.IsAny<Coordinates>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(this.factory.Fixture.CreateOriginLocation()))
            .ReturnsAsync(Result.Ok(this.factory.Fixture.CreateDestinationLocation()));

        Request request = this.factory.Fixture.CreateDraftTripRequest();

        // Act
        Result<CreateDraftResponse> response = await factory.Mediator.Send(request);

        // Assert
        response.Success.Should().BeTrue();
        response.Value.Should().NotBe(Guid.Empty);
    }
}

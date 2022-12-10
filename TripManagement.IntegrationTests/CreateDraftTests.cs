﻿using Arch.SharedKernel.Results;
using AutoFixture;
using FluentAssertions;
using Moq;
using TripManagement.Application.Trips.CreateDraft;
using TripManagement.Contracts.Models;
using TripManagement.Domain.Common;

namespace TripManagement.IntegrationTests;

[Collection(IntegrationTestsConstants.IntegrationTestsCollection)]
public class CreateDraftTests
{
    private readonly TestServicesFactory factory;

    public CreateDraftTests(TestServicesFactory factory)
    {
        this.factory = factory;
        this.factory.DeleteAllTrips();
    }

    [Fact]
    public async Task Draft_trip_is_created()
    {
        // Arrange
        var city = this.factory.Fixture.Create<string>();
        await this.factory.AddCityAsync(city);

        this.factory.CoordinatesAgentMock.Setup(x =>
            x.GetCityByCoordinatesAsync(It.IsAny<Coordinates>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(city);

        this.factory.CoordinatesAgentMock.Setup(x =>
            x.GetLocationByCoordinatesAsync(It.IsAny<Coordinates>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.factory.Fixture.Create<string>());

        CreateDraftRequest draftRequest = this.factory.Fixture.Build<CreateDraftRequest>()
            .With(x => x.OriginLatitude, 41.54)
            .With(x => x.OriginLongitude, 2.10)
            .With(x => x.DestinationLatitude, 41.38)
            .With(x => x.DestinationLongitude, 2.17)
            .Create();
        
        Request request = this.factory.Fixture.Build<Request>().With(x => x.CreateDraft, draftRequest).Create();

        // Act
        Result<CreateDraftResponse> response = await factory.Mediator.Send(request);

        // Assert
        response.Success.Should().BeTrue();
        response.Value.Should().NotBe(Guid.Empty);
    }
}

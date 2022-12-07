using Arch.SharedKernel.Results;
using AutoFixture;
using FluentAssertions;
using MediatR;
using TripManagement.Application.Trips.CreateDraft;
using TripManagement.Contracts.Models;
using TripManagement.Domain.CitiesAggregate;
using TripManagement.Domain.TripsAggregate.Services;

namespace TripManagement.IntegrationTests;

[Collection(IntegrationTestsConstants.IntegrationTestsCollection)]
public class CreateDraftTests
{
    private readonly TestServicesFactory factory;

    public CreateDraftTests(TestServicesFactory factory)
	{
        this.factory = factory;
    }

    [Fact]
    public async Task Draft_trip_is_created()
    {
        // Arrange
        Request request = this.factory.Fixture.Create<Request>();
        var handler = this.factory.GetService<IRequestHandler<Request, Result<CreateDraftResponse>>>();

        //// Act
        //Result<CreateDraftResponse> result = await handler.Handle(request, CancellationToken.None);

        //// Assert
        //result.Value.Should().NotBeNull();
    }
}

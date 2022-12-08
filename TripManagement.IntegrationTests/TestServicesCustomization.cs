using AutoFixture;

namespace TripManagement.IntegrationTests;

public class TestServicesCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<CoordinatesModel>(c => c
            .With(x => x.Latitude, 0)
            .With(x => x.Longitude, 0));
    }
}

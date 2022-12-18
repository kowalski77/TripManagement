using FluentAssertions;
using TripManagement.Domain.Common;

namespace TripManagement.UnitTests.Trips;

public class CoordinatesTests
{
    [Fact]
    public void Distance_calculation_between_two_coordinates_is_correct()
    {
        // Arrange
        const int expectedDistance = 18;
        Coordinates origin = Coordinates.CreateInstance(41.54, 2.10).Value;
        Coordinates destination = Coordinates.CreateInstance(41.38, 2.17).Value;

        // Act
        Kilometers kilometers = origin.DistanceTo(destination);

        // Assert
        _ = kilometers.Value.Should().Be(expectedDistance);
    }

    [Fact]
    public void Distance_between_two_coordintes_with_same_values_is_zero()
    {
        // Arrange
        Coordinates origin = Coordinates.CreateInstance(41.54, 2.10).Value;
        Coordinates destination = Coordinates.CreateInstance(41.54, 2.10).Value;

        // Act
        Kilometers kilometers = origin.DistanceTo(destination);

        // Assert
        _ = kilometers.Value.Should().Be(0);
    }
}

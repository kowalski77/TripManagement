using FluentAssertions;
using TripManagement.Domain.Types;
using TripManagement.Domain.Types.Coordinates;

namespace TripManagement.UnitTests.Trips;

public class CoordinatesTests
{
    [Fact]
    public void Distance_calculation_between_two_coordinates_is_correct()
    {
        // Arrange
        const int expectedDistance = 18;
        Coordinate origin = Coordinate.CreateInstance(41.54, 2.10).Value;
        Coordinate destination = Coordinate.CreateInstance(41.38, 2.17).Value;

        // Act
        Kilometers kilometers = origin.DistanceTo(destination);

        // Assert
        _ = kilometers.Value.Should().Be(expectedDistance);
    }

    [Fact]
    public void Distance_between_two_coordintes_with_same_values_is_zero()
    {
        // Arrange
        Coordinate origin = Coordinate.CreateInstance(41.54, 2.10).Value;
        Coordinate destination = Coordinate.CreateInstance(41.54, 2.10).Value;

        // Act
        Kilometers kilometers = origin.DistanceTo(destination);

        // Assert
        _ = kilometers.Value.Should().Be(0);
    }
}

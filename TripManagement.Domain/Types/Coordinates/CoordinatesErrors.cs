using System.Globalization;
using Arch.SharedKernel;
using Arch.SharedKernel.Results;

namespace TripManagement.Domain.Types.Coordinates;

public static class CoordinatesErrors
{
    public static ErrorResult OutOfRangeCoordinates(string argument, double min, double max) => new(
            CoordinatesErrorConstants.OutOfRangeCoordinatesCode,
            string.Format(CultureInfo.InvariantCulture, CoordinatesErrorConstants.OutOfRangeCoordinatesMessage, argument, min, max));

    public static ErrorResult LocationNameNotRetrieved(Coordinate coordinates) => new(
            CoordinatesErrorConstants.LocationNameCode,
            string.Format(CultureInfo.InvariantCulture, CoordinatesErrorConstants.LocationNameMessage, coordinates.NonNull().Latitude, coordinates.Longitude));
}

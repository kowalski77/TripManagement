using System.Globalization;
using Arch.SharedKernel.Results;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.CitiesAggregate;

public static class CityErrors
{
    public static ErrorResult CityNotFoundWithCoordinates(Coordinates coordinates) => new(
            CityErrorConstants.CityNameNotFoundWithCoordinatesCode,
            string.Format(CultureInfo.InvariantCulture, CityErrorConstants.CityNameNotFoundWithCoordinatesMessage, coordinates.Latitude, coordinates.Longitude));


    public static ErrorResult CityNotAvailable(string name) => new(
            CityErrorConstants.CityNotAvailableCode,
            string.Format(CultureInfo.InvariantCulture, CityErrorConstants.CityNotAvailableMessage, name));

    public static ErrorResult CityNameNullOrEmpty() => new(
        CityErrorConstants.CityNameNullOrEmptyCode,
        string.Format(CultureInfo.InvariantCulture, CityErrorConstants.CityNameNullOrEmptyMessage));
}

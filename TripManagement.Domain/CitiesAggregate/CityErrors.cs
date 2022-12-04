using System.Globalization;
using Arch.SharedKernel.Results;
using TripManagement.Domain.Common;

namespace TripManagement.Domain.CitiesAggregate;

public static class CityErrors
{
    public static ErrorResult CityNotFoundByCoordinates(Coordinates coordinates) => new(
            CityErrorConstants.CityNameNotFoundByCoordinatesCode,
            string.Format(CultureInfo.InvariantCulture, CityErrorConstants.CityNameNotFoundByCoordinatesMessage, coordinates.Latitude, coordinates.Longitude));


    public static ErrorResult CityNotFoundByName(string name) => new(
            CityErrorConstants.CityNameNotFoundCode,
            string.Format(CultureInfo.InvariantCulture, CityErrorConstants.CityNameNotFoundMessage, name));

    public static ErrorResult CityNameNullOrEmpty() => new(
        CityErrorConstants.CityNameNullOrEmptyCode,
        string.Format(CultureInfo.InvariantCulture, CityErrorConstants.CityNameNullOrEmptyMessage));
}

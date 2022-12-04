using System.Globalization;
using Arch.SharedKernel.Results;

namespace TripManagement.Domain.CitiesAggregate;

public static class CityErrors
{
    public static ErrorResult CityNotFoundByName(string name)
    {
        return new ErrorResult(
            CityErrorConstants.CityNameNotFoundCode,
            string.Format(CultureInfo.InvariantCulture, CityErrorConstants.CityNameNotFoundMessage, name));
    }
}

using Arch.SharedKernel.DomainDriven;
using Arch.SharedKernel.Results;

namespace TripManagement.Domain.Models.Coordinates;

public sealed class Coordinates : ValueObject
{
    private const double MinLatitude = -90;
    private const double MaxLatitude = 90;
    private const double MinLongitude = -180;
    private const double MaxLongitude = 180;

    private Coordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; private set; }

    public double Longitude { get; private set; }

    public static Result<Coordinates> CreateInstance(double latitude, double longitude)
    {
        if (latitude is < MinLatitude or > MaxLatitude)
        {
            return CoordinatesErrors.OutOfRangeCoordinates(nameof(latitude), MinLatitude, MaxLatitude);
        }

        if (longitude is < MinLongitude or > MaxLongitude)
        {
            return CoordinatesErrors.OutOfRangeCoordinates(nameof(longitude), MinLongitude, MaxLongitude);
        }

        return new Coordinates(latitude, longitude);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }
}

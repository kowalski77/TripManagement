using Arch.SharedKernel.DomainDriven;
using Arch.SharedKernel.Results;

namespace TripManagement.Domain.Common;

public sealed class Coordinates : ValueObject
{
    private const decimal MinLatitude = -90;
    private const decimal MaxLatitude = 90;
    private const decimal MinLongitude = -180;
    private const decimal MaxLongitude = 180;

    private Coordinates(decimal latitude, decimal longitude)
    {
        this.Latitude = latitude;
        this.Longitude = longitude;
    }

    public decimal Latitude { get; private set; }

    public decimal Longitude { get; private set; }

    public static Result<Coordinates> CreateInstance(decimal latitude, decimal longitude)
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
        yield return this.Latitude;
        yield return this.Longitude;
    }
}

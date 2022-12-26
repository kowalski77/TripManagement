using Arch.SharedKernel;
using Arch.SharedKernel.Results;
using TripManagement.Domain.Types.Coordinates;
using TripManagement.Domain.Types.Locations;

namespace TripManagement.Domain.TripsAggregate;

public static class DraftTrip
{
    public static Result<Trip> Create(Guid id, UserId userId, DateTime pickUp, Location origin, Location destination, TripOptions options) => 
        Result.Init.Validate(
                ValidateMinDistance(origin.NonNull(), destination.NonNull(), options.NonNull()),
                ValidateMaxDistance(origin, destination, options),
                ValidateCityIsAllowed(origin, options),
                ValidateCityIsAllowed(destination, options))
            .OnSuccess(() => new Trip(id, userId, pickUp, origin, destination));

    private static Result ValidateMinDistance(Location origin, Location destination, TripOptions options) =>
        origin.Coordinate.DistanceTo(destination.Coordinate).Value < options.MinDistanceBetweenLocations ?
           TripErrors.DistanceBetweenLocations(options.MinDistanceBetweenLocations, options.MaxDistanceBetweenLocations) :
        Result.Ok();

    private static Result ValidateMaxDistance(Location origin, Location destination, TripOptions options) =>
        origin.Coordinate.DistanceTo(destination.Coordinate).Value > options.MaxDistanceBetweenLocations ?
           TripErrors.DistanceBetweenLocations(options.MinDistanceBetweenLocations, options.MaxDistanceBetweenLocations) :
        Result.Ok();

    private static Result ValidateCityIsAllowed(Location location, TripOptions options) =>
        options.AllowedCities.Contains(location.City.Value) ?
            Result.Ok() :
            TripErrors.CityNotAllowed(location.City.Value);
}

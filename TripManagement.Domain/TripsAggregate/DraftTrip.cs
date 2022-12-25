using Arch.SharedKernel.Results;
using TripManagement.Domain.Types.Coordinates;
using TripManagement.Domain.Types.Locations;

namespace TripManagement.Domain.TripsAggregate;

public static class DraftTrip
{
    public static Result<Trip> Create(Guid id, UserId userId, DateTime pickUp, Location origin, Location destination, TripOptions options)
    {
        Result result = Validate(origin, destination, options);
        return result.Success ?
            new Trip(id, userId, pickUp, origin, destination) :
            result.Error!;
    }

    private static Result Validate(Location origin, Location destination, TripOptions options) => (origin, destination, options) switch
    {
        (var or, var dest, var opt) when or.Coordinate.DistanceTo(dest.Coordinate).Value < opt.MinDistanceBetweenLocations =>
            TripErrors.DistanceBetweenLocations(opt.MinDistanceBetweenLocations, opt.MaxDistanceBetweenLocations),
        (var or, var dest, var opt) when or.Coordinate.DistanceTo(dest.Coordinate).Value > opt.MaxDistanceBetweenLocations =>
            TripErrors.DistanceBetweenLocations(opt.MinDistanceBetweenLocations, opt.MaxDistanceBetweenLocations),
        (var or, _, var opt) when !opt.AllowedCities.Contains(or.City.Value) =>
            TripErrors.CityNotAllowed(or.City.Value),
        (_, var dest, var opt) when !opt.AllowedCities.Contains(dest.City.Value) =>
            TripErrors.CityNotAllowed(dest.City.Value),
        _ =>
            Result.Ok()
    };
}

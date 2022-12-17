using Arch.SharedKernel.Results;
using TripManagement.Domain.LocationsAggregate;
using TripManagement.Domain.Models.Coordinates;

namespace TripManagement.Domain.TripsAggregate;

public static class TripExtensions
{
    public static Result Validate(Location origin, Location destination, TripOptions options) => (origin, destination, options) switch
    {
        (var o, var d, var opt) when o.Coordinates.DistanceTo(d.Coordinates).Value < opt.MinDistanceBetweenLocations =>
            TripErrors.DistanceBetweenLocations(opt.MinDistanceBetweenLocations, opt.MaxDistanceBetweenLocations),
        (var o, var d, var opt) when o.Coordinates.DistanceTo(d.Coordinates).Value > opt.MaxDistanceBetweenLocations =>
            TripErrors.DistanceBetweenLocations(opt.MinDistanceBetweenLocations, opt.MaxDistanceBetweenLocations),
        (var o, _, var opt) when !opt.AllowedCities.Contains(o.City.Value) =>
            TripErrors.CityNotAllowed(o.City.Value),
        (_, var d, var opt) when !opt.AllowedCities.Contains(d.City.Value) =>
            TripErrors.CityNotAllowed(d.City.Value),
        _ =>
            Result.Ok()
    };
}

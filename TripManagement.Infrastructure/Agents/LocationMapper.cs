using Geocoding.Contracts;
using TripManagement.Domain.Types.Coordinates;
using TripManagement.Domain.Types.Locations;
using Location = TripManagement.Domain.Types.Locations.Location;

namespace TripManagement.Infrastructure.Agents;

internal static class LocationMapper
{
    public static Location ToLocation(this Place place) => Location.Create(
        Guid.NewGuid(),
        new Address(place.AddressComponent.Name),
        new City(place.AddressComponent.City),
        new PlaceId(place.PlaceId),
        Coordinate.CreateInstance(place.Geometry.Location.Latitude, place.Geometry.Location.Longitude).Value);
}

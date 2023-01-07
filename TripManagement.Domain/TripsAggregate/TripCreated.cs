using MediatR;
using TripManagement.Domain.Types.Locations;

namespace TripManagement.Domain.TripsAggregate;

public sealed record TripCreated(Guid Id, UserId UserId, DateTime PickUp, Location Origin, Location Destination) : INotification;
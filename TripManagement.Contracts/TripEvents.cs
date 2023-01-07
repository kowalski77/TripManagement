namespace TripManagement.Contracts;

public record struct TripCreated(Guid Id, Guid UserId, DateTime PickUp, string Origin, string Destination);
namespace TripManagement.Contracts.Models;

public sealed record CreateDraftRequest(Guid UserId, DateTime PickUp, decimal OriginLatitude, decimal OriginLongitude, decimal DestinationLatitude, decimal DestinationLongitude);

public sealed record CreateDraftResponse(Guid TripId);
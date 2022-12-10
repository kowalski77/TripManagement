namespace TripManagement.Contracts.Models;

public record struct CreateDraftRequest(Guid UserId, DateTime PickUp, 
    double OriginLatitude,
    double OriginLongitude,
    double DestinationLatitude,
    double DestinationLongitude);

public record struct CreateDraftResponse(Guid TripId);
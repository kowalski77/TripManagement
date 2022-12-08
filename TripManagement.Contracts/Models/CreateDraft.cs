namespace TripManagement.Contracts.Models;

public record struct CreateDraftRequest(Guid UserId, DateTime PickUp, CoordinatesModel Origin, CoordinatesModel Destination);

public record struct CreateDraftResponse(Guid TripId);
namespace TripManagement.Contracts;

public record struct CreateDraftTripRequest(
    Guid UserId,
    DateTime PickUp,
    double OriginLatitude,
    double OriginLongitude,
    double DestinationLatitude,
    double DestinationLongitude);

public record struct CreateDraftTripResponse(Guid TripId);

public record struct ConfirmTripRequest(Guid TripId);
namespace TripManagement.Domain.TripsAggregate;

public record DraftTripDto(Guid Id);

public record TripDto(Guid UserId, string? DriverName, DateTime PickUpTime, TripByIdLocationDto Origin, TripByIdLocationDto Destination, int Credits);

public record TripSummaryDto(Guid UserId, string? DriverName, string Status, string Origin, string Destination);

public record TripByIdLocationDto(string Name, string City, decimal Longitude, decimal Latitude);

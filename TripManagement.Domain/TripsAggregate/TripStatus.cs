namespace TripManagement.Domain.TripsAggregate;

public enum TripStatus
{
    Draft,
    Confirmed,
    ToOrigin,
    ToDestination,
    Canceled,
    Finished
}

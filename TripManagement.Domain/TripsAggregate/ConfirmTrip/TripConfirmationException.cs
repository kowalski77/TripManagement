using System.Runtime.Serialization;

namespace TripManagement.Domain.TripsAggregate.ConfirmTrip;

[Serializable]
public class TripConfirmationException : Exception
{
    public TripConfirmationException()
    {
    }

    public TripConfirmationException(string? message) : base(message)
    {
    }

    public TripConfirmationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected TripConfirmationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
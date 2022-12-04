using System.Runtime.Serialization;

namespace TripManagement.Domain.TripsAggregate.Exceptions;
[Serializable]
public class TripInvalidationException : Exception
{
    public TripInvalidationException()
    {
    }

    public TripInvalidationException(string? message) : base(message)
    {
    }

    public TripInvalidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected TripInvalidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
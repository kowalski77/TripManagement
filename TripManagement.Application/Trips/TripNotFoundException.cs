using System.Runtime.Serialization;

namespace TripManagement.Application.Trips;

[Serializable]
public class TripNotFoundException : Exception
{
    public TripNotFoundException()
    {
    }

    public TripNotFoundException(Guid id) : base($"Trip with id {id} not found")
    {
    }

    public TripNotFoundException(string? message) : base(message)
    {
    }

    public TripNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected TripNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
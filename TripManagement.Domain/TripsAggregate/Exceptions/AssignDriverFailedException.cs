using System.Runtime.Serialization;

namespace TripManagement.Domain.TripsAggregate.Exceptions;

[Serializable]
public class AssignDriverFailedException : Exception
{
    public AssignDriverFailedException()
    {
    }

    public AssignDriverFailedException(string? message) : base(message)
    {
    }

    public AssignDriverFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected AssignDriverFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
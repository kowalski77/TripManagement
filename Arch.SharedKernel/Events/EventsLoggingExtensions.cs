using Microsoft.Extensions.Logging;

namespace Arch.SharedKernel.Events;

public static partial class EventsLoggingExtensions
{
    [LoggerMessage(0, LogLevel.Information, "Event published with content: {json}")]
    public static partial void LogPublishedEvent(this ILogger logger, string json);
}

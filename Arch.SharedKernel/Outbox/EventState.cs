namespace Arch.SharedKernel.Outbox;

public enum EventState
{
    NotPublished = 0,
    Published = 1,
    PublishedFailed = 2
}

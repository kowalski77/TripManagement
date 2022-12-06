using MediatR;

namespace Arch.SharedKernel.Mediator;

public interface IDomainNotificationHandler<in T> : INotificationHandler<T>
    where T : IDomainNotification
{
}

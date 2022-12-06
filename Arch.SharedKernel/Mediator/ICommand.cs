using MediatR;

namespace Arch.SharedKernel.Mediator;

public interface ICommand<out TCommand> : IRequest<TCommand>
{
}

using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace TripManagement.Application.Behaviors;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IDbContext dbContext;

    public TransactionBehaviour(IDbContext dbContext) => this.dbContext = dbContext;

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) => 
        this.HandleInternal(next.NonNull(), cancellationToken);

    private async Task<TResponse> HandleInternal(RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        IExecutionStrategy strategy = dbContext.DatabaseFacade.CreateExecutionStrategy();

        TResponse? response = await strategy.ExecuteAsync(async () =>
                await ExecuteTransactionAsync(next, cancellationToken).ConfigureAwait(false))
            .ConfigureAwait(false);

        return response ?? throw new InvalidOperationException("Response is null");
    }

    private async Task<TResponse> ExecuteTransactionAsync(RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await using IDbContextTransaction transaction = await dbContext.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

        TResponse? response = await next().ConfigureAwait(false);
        await dbContext.CommitTransactionAsync(transaction, cancellationToken).ConfigureAwait(false);

        return response;
    }
}

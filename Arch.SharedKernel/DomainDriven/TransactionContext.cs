using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Arch.SharedKernel.DomainDriven;

public abstract class TransactionContext : DbContext, IDbContext, IUnitOfWork
{
    private readonly IMediator mediator;
    private IDbContextTransaction? currentTransaction;

    protected TransactionContext(
        DbContextOptions options,
        IMediator mediator)
        : base(options) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public DatabaseFacade DatabaseFacade => base.Database;

    public IDbContextTransaction GetCurrentTransaction() => currentTransaction is null ? throw new InvalidOperationException("Current transaction is null") : currentTransaction;

    public virtual async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction is not null)
        {
            throw new InvalidOperationException("There is already a transaction in progress.");
        }

        currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken)
            .ConfigureAwait(false);

        return currentTransaction;
    }

    public virtual Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default) =>
        transaction.NonNull() != currentTransaction
            ? throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current")
            : TryCommitTransactionAsync(transaction, cancellationToken);

    public virtual async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await mediator.PublishDomainEventsAsync(this, cancellationToken).ConfigureAwait(false);

        return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
    }

    private async Task TryCommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
    {
        try
        {
            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (currentTransaction is not null)
            {
                currentTransaction.Dispose();
                currentTransaction = null;
            }
        }
    }

    private void RollbackTransaction()
    {
        try
        {
            currentTransaction?.Rollback();
        }
        finally
        {
            if (currentTransaction is not null)
            {
                currentTransaction.Dispose();
                currentTransaction = null;
            }
        }
    }
}

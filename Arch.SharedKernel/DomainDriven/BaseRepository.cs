namespace Arch.SharedKernel.DomainDriven;

public class BaseRepository<T> : IRepository<T>
    where T : class, IAggregateRoot
{
    public BaseRepository(TransactionContext context) => this.Context = context ?? throw new ArgumentNullException(nameof(context));

    protected TransactionContext Context { get; }

    public IUnitOfWork UnitOfWork => this.Context;

    public virtual T Add(T item) => 
        this.Context.Add(item).Entity;

    public virtual async Task<Maybe<T>> GetAsync(Guid id, CancellationToken cancellationToken = default) => 
        await this.Context.FindAsync<T>(new object[] { id }, cancellationToken).ConfigureAwait(false);
}

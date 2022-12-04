namespace Arch.SharedKernel.DomainDriven;

public interface IRepository<T>
    where T : class, IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }

    T Add(T item);

    Task<Maybe<T>> GetAsync(Guid id, CancellationToken cancellationToken = default);
}

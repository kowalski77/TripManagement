using Arch.SharedKernel.DomainDriven;
using Microsoft.EntityFrameworkCore;

namespace Arch.SharedKernel.Outbox;

public class OutboxContext : DbContext, IUnitOfWork
{
    public OutboxContext(DbContextOptions<OutboxContext> options)
        : base(options)
    {
    }

    public DbSet<OutboxMessage> OutboxMessages { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.NonNull().ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) => await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
}

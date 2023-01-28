using Microsoft.EntityFrameworkCore;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks;

public class BuildingBlocksDbContext : DbContext
{
    public DbSet<OutboxMessage> OutboxMessages { get; internal init; }
    public DbSet<AggregateRootHistoryItem> AggregateRootHistory { get; internal init; }
    private bool _commited;

    public BuildingBlocksDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public void SetPartitionKey<TEntity, TId>(TEntity entity, TId id)
        where TEntity : class
        where TId : class
    {
        Entry(entity).Property("partitionKey").CurrentValue = id.ToString();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (_commited)
        {
            throw new Exception("can not commit twice within a scope in DbContext");
        }
        _commited = true;
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        if (_commited)
        {
            throw new Exception("can not commit twice within a scope in DbContext");
        }
        _commited = true;
        return base.SaveChanges();
    }
}

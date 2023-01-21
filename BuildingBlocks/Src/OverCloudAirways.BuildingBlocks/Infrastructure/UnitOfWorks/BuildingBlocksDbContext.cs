using Microsoft.EntityFrameworkCore;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace DArch.UnitOfWorks.EFCore;

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
        base.OnModelCreating(modelBuilder);
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

using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks;

internal class UnitOfWork : IUnitOfWork
{
    private readonly BuildingBlocksDbContext _context;

    public UnitOfWork(BuildingBlocksDbContext context)
    {
        _context = context;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}

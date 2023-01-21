namespace OverCloudAirways.BuildingBlocks.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken);
}

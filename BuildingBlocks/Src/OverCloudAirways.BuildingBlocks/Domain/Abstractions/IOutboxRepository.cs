using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Domain.Abstractions;

public interface IOutboxRepository
{
    Task AddAsync(OutboxMessage message);
    Task<OutboxMessage?> LoadAsync(Guid id, CancellationToken cancellationToken);
    void Remove(OutboxMessage outboxMessage);
}

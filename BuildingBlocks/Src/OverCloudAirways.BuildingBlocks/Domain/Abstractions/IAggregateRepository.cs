using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Domain.Abstractions;

public interface IAggregateRepository
{
    Task AddAsync<TAggregateRoot, TKey>(TAggregateRoot aggregateRoot)
        where TAggregateRoot : AggregateRoot<TKey>
        where TKey : TypedId;

    Task<TAggregateRoot> LoadAsync<TAggregateRoot, TKey>(TKey aggregateId)
        where TAggregateRoot : AggregateRoot<TKey>
        where TKey : TypedId;
}

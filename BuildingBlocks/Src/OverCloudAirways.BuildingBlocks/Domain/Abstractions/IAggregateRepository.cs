using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Domain.Abstractions;

public interface IAggregateRepository
{
    void Add<TAggregateRoot, TKey>(TAggregateRoot aggregateRoot)
        where TAggregateRoot : AggregateRoot<TKey>
        where TKey : TypedId;

    Task<TAggregateRoot> LoadAsync<TAggregateRoot, TKey>(TKey aggregateId)
        where TAggregateRoot : AggregateRoot<TKey>
        where TKey : TypedId;

    IAggregateRoot? GetModifiedAggregate();
}

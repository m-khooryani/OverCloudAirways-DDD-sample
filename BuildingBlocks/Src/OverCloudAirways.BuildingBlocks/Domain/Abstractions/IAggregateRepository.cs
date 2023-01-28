using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Domain.Abstractions;

public interface IAggregateRepository
{
    void Add<TAggregateRoot>(TAggregateRoot aggregateRoot)
        where TAggregateRoot : IAggregateRoot;

    Task<TAggregateRoot> LoadAsync<TAggregateRoot, TKey>(TKey aggregateId)
        where TAggregateRoot : AggregateRoot<TKey>
        where TKey : TypedId;

    IAggregateRoot? GetModifiedAggregate();
}

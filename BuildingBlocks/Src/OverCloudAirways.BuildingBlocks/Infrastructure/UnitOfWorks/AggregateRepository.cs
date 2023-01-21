using DArch.Infrastructure.CleanArchitecture;
using DArch.UnitOfWorks.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.BuildingBlocks.Domain.Exceptions;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks;

internal class AggregateRepository : IAggregateRepository
{
    private readonly Dictionary<TypedId, IAggregateRoot> _loadedAggregates;
    private readonly ILogger _logger;
    private readonly BuildingBlocksDbContext _context;
    private readonly AssemblyLayers _layers;
    private const int ChunkSize = 100;

    public AggregateRepository(
        ILogger logger,
        BuildingBlocksDbContext context,
        AssemblyLayers layers)
    {
        _logger = logger;
        _loadedAggregates = new();
        _context = context;
        _layers = layers;
    }

    public void Add<TAggregateRoot, TKey>(TAggregateRoot aggregateRoot)
        where TAggregateRoot : AggregateRoot<TKey>
        where TKey : TypedId
    {
        _loadedAggregates.Add(aggregateRoot.Id, aggregateRoot);
    }

    public async Task<TAggregateRoot> LoadAsync<TAggregateRoot, TKey>(TKey aggregateId)
        where TAggregateRoot : AggregateRoot<TKey>
        where TKey : TypedId
    {
        if (_loadedAggregates.ContainsKey(aggregateId))
        {
            return _loadedAggregates[aggregateId] as TAggregateRoot;
        }
        var aggregateRoot = await Load<TAggregateRoot, TKey>(aggregateId);

        _loadedAggregates.Add(aggregateId, aggregateRoot);
        return aggregateRoot;
    }

    public IAggregateRoot? GetModifiedAggregate()
    {
        var modifiedAggregates = _loadedAggregates.Values
            .Where(aggregate => aggregate.DomainEvents.Any());
        if (modifiedAggregates.Count() > 1)
        {
            throw new Exception("Updating multiple aggregates in a transaction is not allowed");
        }
        return modifiedAggregates.FirstOrDefault();
    }

    private async Task<TAggregateRoot> Load<TAggregateRoot, TId>(TId aggregateId)
        where TAggregateRoot : AggregateRoot<TId>
        where TId : TypedId
    {
        var history = await LoadHistoryAsync(aggregateId);
        if (!history.Any())
        {
            throw new EntityNotFoundException($"Entity not found with Id: {aggregateId}");
        }

        var aggregate = CreateAggregateDynamically<TAggregateRoot, TId>(history);

        return RestoreAggregateStateFromHistory<TAggregateRoot, TId>(history, aggregate);
    }

    private TAggregateRoot? CreateAggregateDynamically<TAggregateRoot, TId>(List<AggregateRootHistoryItem> history)
        where TAggregateRoot : AggregateRoot<TId>
        where TId : TypedId
    {
        var aggregateType = history.First().AggregateType;
        var type = _layers.DomainLayer.GetType(aggregateType);
        var aggregate = (TAggregateRoot)Activator.CreateInstance(type, true);
        return aggregate;
    }

    private TAggregateRoot RestoreAggregateStateFromHistory<TAggregateRoot, TId>(List<AggregateRootHistoryItem> history, TAggregateRoot? aggregate)
        where TAggregateRoot : AggregateRoot<TId>
        where TId : TypedId
    {
        _logger.LogInformation("Restoring aggregate state by applying {eventsCount}", history.Count);
        var domainEvents = new Queue<DomainEvent>();
        foreach (var historyItem in history)
        {
            _logger.LogDebug("Applying {eventName}...", historyItem.EventType);
            var domainEventType = _layers.DomainLayer.GetType(historyItem.EventType);
            var domainEvent = JsonConvert.DeserializeObject(historyItem.Data, domainEventType);

            domainEvents.Enqueue(domainEvent! as DomainEvent);
        }
        aggregate.Load(domainEvents);
        return aggregate;
    }

    private async Task<List<AggregateRootHistoryItem>> LoadHistoryAsync<TId>(TId aggregateId) where TId : TypedId
    {
        var historyChunks = AsyncChunk(ChunkSize,
            _context.AggregateRootHistory
                .Where(x => x.AggregateId == aggregateId.ToString())
                .OrderBy(x => x.Datetime));

        var history = new List<AggregateRootHistoryItem>();
        await foreach (var historyChunk in historyChunks)
        {
            history.AddRange(historyChunk);
        }

        return history;
    }

    private static async IAsyncEnumerable<IEnumerable<TSource>> AsyncChunk<TSource>(int chunkSize, IOrderedQueryable<TSource> source)
    {
        for (int i = 0; i < source.Count(); i += chunkSize)
        {
            yield return await source
                .Skip(i)
                .Take(chunkSize)
                .ToArrayAsync();
        }
    }
}

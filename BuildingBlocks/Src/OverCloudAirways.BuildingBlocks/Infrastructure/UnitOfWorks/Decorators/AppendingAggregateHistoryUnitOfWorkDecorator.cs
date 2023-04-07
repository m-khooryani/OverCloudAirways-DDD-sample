using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks.Decorators;

internal class AppendingAggregateHistoryUnitOfWorkDecorator : IUnitOfWork
{
    private readonly IUnitOfWork _decorated;
    private readonly IUserAccessor _userAccessor;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly BuildingBlocksDbContext _context;

    public AppendingAggregateHistoryUnitOfWorkDecorator(
        IUnitOfWork decorated,
        IUserAccessor userAccessor,
        IJsonSerializer jsonSerializer,
        IAggregateRepository aggregateRepository,
        BuildingBlocksDbContext context)
    {
        _decorated = decorated;
        _userAccessor = userAccessor;
        _jsonSerializer = jsonSerializer;
        _aggregateRepository = aggregateRepository;
        _context = context;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        await AppendHistoryAsync(cancellationToken);
        return await _decorated.CommitAsync(cancellationToken);
    }

    private async Task AppendHistoryAsync(CancellationToken cancellationToken)
    {
        var aggregate = _aggregateRepository.GetModifiedAggregate();
        if (aggregate is null)
        {
            return;
        }
        var domainEvents = aggregate.DomainEvents;
        var type = aggregate.GetType().FullName;
        var index = 0;
        foreach (var domainEvent in domainEvents)
        {
            var data = _jsonSerializer.Serialize(domainEvent);
            var historyItem = AggregateRootHistoryItem.Create(
                domainEvent.AggregateId,
                aggregate.Version + index,
                _userAccessor.UserId,
                _userAccessor.FullName,
                domainEvent.GetType().FullName!,
                domainEvent.OccurredAt,
                type!,
                data);
            await _context.AggregateRootHistory
                .AddAsync(historyItem, cancellationToken);
            index++;
        }
    }
}

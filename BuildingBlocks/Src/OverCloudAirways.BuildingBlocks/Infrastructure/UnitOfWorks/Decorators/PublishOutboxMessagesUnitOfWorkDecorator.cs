using DArch.AzureServiceBus;
using DArch.Infrastructure.EventBus;
using DArch.UnitOfWorks.EFCore;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks.Decorators;

internal class PublishOutboxMessagesUnitOfWorkDecorator : IUnitOfWork
{
    private readonly IUnitOfWork _decorated;
    private readonly BuildingBlocksDbContext _context;
    private readonly IEventBus _eventBus;
    private readonly ServiceBusConfig _config;

    public PublishOutboxMessagesUnitOfWorkDecorator(
        IUnitOfWork decorated,
        BuildingBlocksDbContext context,
        IEventBus eventBus,
        ServiceBusConfig config)
    {
        _decorated = decorated;
        _context = context;
        _eventBus = eventBus;
        _config = config;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        var outboxMessages = _context
            .ChangeTracker
            .Entries<OutboxMessage>()
            .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
            .Select(x => x.Entity);

        var changes = await _decorated.CommitAsync(cancellationToken);

        outboxMessages
            .ToList()
            .ForEach(async outboxMessage => await _eventBus.PublishAsync(
                _config.OutboxQueueName,
                outboxMessage.SessionId,
                outboxMessage));

        return changes;
    }
}
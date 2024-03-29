﻿using Microsoft.EntityFrameworkCore;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks;

internal class OutboxRepository : IOutboxRepository
{
    private readonly BuildingBlocksDbContext _context;

    public OutboxRepository(BuildingBlocksDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(OutboxMessage message)
    {
        _context.SetPartitionKey(message, message.Id.ToString());
        await _context.OutboxMessages.AddAsync(message);
    }

    public async Task<OutboxMessage?> LoadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .OutboxMessages
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public void Remove(OutboxMessage outboxMessage)
    {
        _context.OutboxMessages.Remove(outboxMessage);
    }
}

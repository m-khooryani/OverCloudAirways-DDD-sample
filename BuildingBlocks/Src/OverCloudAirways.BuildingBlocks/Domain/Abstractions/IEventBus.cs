using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace DArch.Infrastructure.EventBus;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
    internal Task PublishAsync(string queue, string sessionId, OutboxMessage outboxMessage);
}

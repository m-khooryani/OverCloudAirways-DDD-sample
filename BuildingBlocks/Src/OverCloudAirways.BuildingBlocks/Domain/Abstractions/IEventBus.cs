using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Domain.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
    internal Task PublishAsync(string queue, OutboxMessage outboxMessage);
    Task<string> ScheduleAsync(string queue, OutboxMessage outboxMessage, DateTimeOffset dateTimeOffset);
}

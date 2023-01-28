using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BuildingBlocks.Domain.Models;

public record IntegrationEvent
{
    public TypedId AggregateId { get; }
    public Guid IntegrationEventId { get; }
    public DateTimeOffset OccurredOn { get; }
    public string IntegrationEventName { get; }

    protected IntegrationEvent(
        TypedId aggreateId,
        string integrationEventName)
    {
        AggregateId = aggreateId;
        IntegrationEventId = Guid.NewGuid();
        OccurredOn = Clock.Now;
        IntegrationEventName = integrationEventName;
    }
}

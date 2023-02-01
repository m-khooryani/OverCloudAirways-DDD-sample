using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BuildingBlocks.Domain.Models;

public record IntegrationEvent
{
    public string AggregateId { get; }
    public Guid IntegrationEventId { get; }
    public DateTimeOffset OccurredOn { get; }
    public string IntegrationEventName { get; }

    private IntegrationEvent(
        string aggreateId,
        Guid integrationEventId,
        DateTimeOffset occurredOn,
        string integrationEventName)
    {
        AggregateId = aggreateId;
        IntegrationEventId = integrationEventId;
        OccurredOn = occurredOn;
        IntegrationEventName = integrationEventName;
    }

    protected IntegrationEvent(
        TypedId aggreateId,
        string integrationEventName)
        : this(aggreateId.ToString()!, Guid.NewGuid(), Clock.Now, integrationEventName)
    {
    }
}

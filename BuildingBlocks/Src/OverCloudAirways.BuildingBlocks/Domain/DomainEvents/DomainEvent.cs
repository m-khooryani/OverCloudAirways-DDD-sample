using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

public record DomainEvent
{
    public DateTimeOffset OccurredAt { get; }
    public string AggregateId { get; }

    private DomainEvent(string aggregateId)
    {
        AggregateId = aggregateId;
        OccurredAt = Clock.Now;
    }

    public DomainEvent(TypedId aggregateId)
        : this(aggregateId.ToString())
    {
    }
}

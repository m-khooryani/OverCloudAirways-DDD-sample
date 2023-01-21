using MediatR;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

public class DomainEventPolicy<T> : INotification
    where T : DomainEvent
{
    public Guid Id { get; }
    public T DomainEvent { get; }

    public DomainEventPolicy(T domainEvent)
    {
        Id = Guid.NewGuid();
        DomainEvent = domainEvent;
    }
}

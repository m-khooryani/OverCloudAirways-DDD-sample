using MediatR;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

public interface IDomainPolicyHandler<TNotification, TEvent> : INotificationHandler<TNotification>
    where TNotification : DomainEventPolicy<TEvent>
    where TEvent : DomainEvent
{
}

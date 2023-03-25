using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Canceled;

public class OrderCanceledPolicy : DomainEventPolicy<OrderCanceledDomainEvent>
{
    public OrderCanceledPolicy(OrderCanceledDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

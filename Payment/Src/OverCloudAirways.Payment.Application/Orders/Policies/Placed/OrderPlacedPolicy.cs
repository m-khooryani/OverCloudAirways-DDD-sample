using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Placed;

public class OrderPlacedPolicy : DomainEventPolicy<OrderPlacedDomainEvent>
{
    public OrderPlacedPolicy(OrderPlacedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

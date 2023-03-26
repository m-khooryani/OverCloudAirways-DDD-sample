using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Failed;

public class OrderFailedPolicy : DomainEventPolicy<OrderFailedDomainEvent>
{
    public OrderFailedPolicy(OrderFailedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

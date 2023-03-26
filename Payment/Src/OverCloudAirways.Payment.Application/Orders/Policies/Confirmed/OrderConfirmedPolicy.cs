using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Confirmed;

public class OrderConfirmedPolicy : DomainEventPolicy<OrderConfirmedDomainEvent>
{
    public OrderConfirmedPolicy(OrderConfirmedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

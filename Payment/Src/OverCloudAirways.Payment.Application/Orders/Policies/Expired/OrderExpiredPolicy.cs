using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Expired;

public class OrderExpiredPolicy : DomainEventPolicy<OrderExpiredDomainEvent>
{
    public OrderExpiredPolicy(OrderExpiredDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

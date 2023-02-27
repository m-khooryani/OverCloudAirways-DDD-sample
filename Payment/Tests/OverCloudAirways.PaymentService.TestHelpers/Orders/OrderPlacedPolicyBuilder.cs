using OverCloudAirways.PaymentService.Application.Orders.Policies.Placed;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderPlacedPolicyBuilder
{
    private OrderPlacedDomainEvent _domainEvent = new OrderPlacedDomainEventBuilder().Build();

    public OrderPlacedPolicy Build()
    {
        return new OrderPlacedPolicy(_domainEvent);
    }

    public OrderPlacedPolicyBuilder SetDomainEvent(OrderPlacedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}

using OverCloudAirways.PaymentService.Application.Orders.Policies.Confirmed;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderConfirmedPolicyBuilder
{
    private OrderConfirmedDomainEvent _domainEvent = new OrderConfirmedDomainEventBuilder().Build();

    public OrderConfirmedPolicy Build()
    {
        return new OrderConfirmedPolicy(_domainEvent);
    }

    public OrderConfirmedPolicyBuilder SetDomainEvent(OrderConfirmedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}

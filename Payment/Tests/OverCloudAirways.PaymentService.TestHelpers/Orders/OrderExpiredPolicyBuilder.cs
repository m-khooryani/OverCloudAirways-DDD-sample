using OverCloudAirways.PaymentService.Application.Orders.Policies.Expired;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderExpiredPolicyBuilder
{
    private OrderExpiredDomainEvent _domainEvent = new OrderExpiredDomainEventBuilder().Build();

    public OrderExpiredPolicy Build()
    {
        return new OrderExpiredPolicy(_domainEvent);
    }

    public OrderExpiredPolicyBuilder SetDomainEvent(OrderExpiredDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
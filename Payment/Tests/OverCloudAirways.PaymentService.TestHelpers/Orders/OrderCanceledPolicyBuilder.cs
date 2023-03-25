using OverCloudAirways.PaymentService.Application.Orders.Policies.Canceled;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderCanceledPolicyBuilder
{
    private OrderCanceledDomainEvent _domainEvent = new OrderCanceledDomainEventBuilder().Build();

    public OrderCanceledPolicy Build()
    {
        return new OrderCanceledPolicy(_domainEvent);
    }

    public OrderCanceledPolicyBuilder SetDomainEvent(OrderCanceledDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
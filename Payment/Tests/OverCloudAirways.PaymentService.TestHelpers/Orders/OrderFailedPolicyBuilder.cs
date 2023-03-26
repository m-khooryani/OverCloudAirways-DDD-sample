using OverCloudAirways.PaymentService.Application.Orders.Policies.Failed;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderFailedPolicyBuilder
{
    private OrderFailedDomainEvent _domainEvent = new OrderFailedDomainEventBuilder().Build();

    public OrderFailedPolicy Build()
    {
        return new OrderFailedPolicy(_domainEvent);
    }

    public OrderFailedPolicyBuilder SetDomainEvent(OrderFailedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}

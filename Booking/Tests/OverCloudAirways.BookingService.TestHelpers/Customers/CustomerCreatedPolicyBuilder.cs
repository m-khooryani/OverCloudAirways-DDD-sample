using OverCloudAirways.BookingService.Application.Customers.Policies.Created;
using OverCloudAirways.BookingService.Domain.Customers.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Customers;

public class CustomerCreatedPolicyBuilder
{
    private CustomerCreatedDomainEvent _domainEvent = new CustomerCreatedDomainEventBuilder().Build();

	public CustomerCreatedPolicy Build()
    {
        return new CustomerCreatedPolicy(_domainEvent);
    }

    public CustomerCreatedPolicyBuilder SetDomainEvent(CustomerCreatedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}

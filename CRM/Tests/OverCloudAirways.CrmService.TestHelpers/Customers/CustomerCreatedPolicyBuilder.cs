using OverCloudAirways.CrmService.Application.Customers.Policies.Created;
using OverCloudAirways.CrmService.Domain.Customers.Events;

namespace OverCloudAirways.CrmService.TestHelpers.Customers;

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
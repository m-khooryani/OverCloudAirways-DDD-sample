using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.CrmService.Domain.Customers.Events;

namespace OverCloudAirways.CrmService.Application.Customers.Policies.Created;

public class CustomerCreatedPolicy : DomainEventPolicy<CustomerCreatedDomainEvent>
{
    public CustomerCreatedPolicy(CustomerCreatedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

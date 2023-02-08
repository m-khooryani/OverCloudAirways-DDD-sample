using OverCloudAirways.BookingService.Domain.Customers.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.Customers.Policies.Created;

public class CustomerCreatedPolicy : DomainEventPolicy<CustomerCreatedDomainEvent>
{
    public CustomerCreatedPolicy(CustomerCreatedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

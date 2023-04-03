using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Policies.QualifiedForCustomer;

public class LoyaltyProgramQualifiedForCustomerPolicy : DomainEventPolicy<LoyaltyProgramQualifiedForCustomerDomainEvent>
{
    public LoyaltyProgramQualifiedForCustomerPolicy(LoyaltyProgramQualifiedForCustomerDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Policies.Planned;

public class LoyaltyProgramPlannedPolicy : DomainEventPolicy<LoyaltyProgramPlannedDomainEvent>
{
    public LoyaltyProgramPlannedPolicy(LoyaltyProgramPlannedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

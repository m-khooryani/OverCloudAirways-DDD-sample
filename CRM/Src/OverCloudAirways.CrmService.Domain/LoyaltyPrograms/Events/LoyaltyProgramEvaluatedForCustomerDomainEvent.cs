using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.CrmService.Domain.Customers;

namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;

public record LoyaltyProgramEvaluatedForCustomerDomainEvent(
    LoyaltyProgramId LoyaltyProgramId, 
    CustomerId CustomerId) : DomainEvent(LoyaltyProgramId);

using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;

public record LoyaltyProgramPlannedDomainEvent(
    LoyaltyProgramId LoyaltyProgramId, 
    string Name,
    decimal PurchaseRequirements,
    Percentage DiscountPercentage) : DomainEvent(LoyaltyProgramId);

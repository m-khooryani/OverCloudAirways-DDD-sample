using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;

public record LoyaltyProgramReactivatedDomainEvent(LoyaltyProgramId LoyaltyProgramId) : DomainEvent(LoyaltyProgramId);

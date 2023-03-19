using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;

public record LoyaltyProgramSuspendedDomainEvent(LoyaltyProgramId LoyaltyProgramId) : DomainEvent(LoyaltyProgramId);

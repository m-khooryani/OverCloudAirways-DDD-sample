using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.CrmService.Domain.Customers.Events;

public record CustomerLoyaltyPointsCollectedDomainEvent(
    CustomerId CustomerId,
    decimal LoyaltyPoints) : DomainEvent(CustomerId);

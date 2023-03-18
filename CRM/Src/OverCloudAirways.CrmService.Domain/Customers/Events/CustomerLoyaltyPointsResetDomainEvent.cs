using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.CrmService.Domain.Customers.Events;

public record CustomerLoyaltyPointsResetDomainEvent(CustomerId CustomerId) : DomainEvent(CustomerId);

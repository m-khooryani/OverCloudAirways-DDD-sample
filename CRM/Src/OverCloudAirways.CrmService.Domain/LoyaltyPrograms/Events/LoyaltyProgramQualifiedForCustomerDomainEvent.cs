using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.CrmService.Domain.Customers;

namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;

public record LoyaltyProgramQualifiedForCustomerDomainEvent(
    LoyaltyProgramId LoyaltyProgramId, 
    CustomerId CustomerId,
    Percentage DiscountPercentage) : DomainEvent(LoyaltyProgramId);

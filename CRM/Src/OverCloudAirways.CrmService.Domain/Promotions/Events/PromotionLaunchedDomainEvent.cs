using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Domain.Promotions.Events;

public record PromotionLaunchedDomainEvent(
    PromotionId PromotionId,
    string DiscountCode,
    Percentage DiscountPercentage,
    string? Description,
    CustomerId? CustomerId,
    DateTimeOffset ExpirationDate) : DomainEvent(PromotionId);

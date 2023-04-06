using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;
using OverCloudAirways.CrmService.Domain.Promotions;

namespace OverCloudAirways.CrmService.IntegrationEvents.Promotions;

public record PromotionLaunchedIntegrationEvent(
    PromotionId PromotionId,
    string DiscountCode,
    Percentage DiscountPercentage,
    string? Description,
    CustomerId? CustomerId,
    DateTimeOffset ExpirationDate) : IntegrationEvent(PromotionId, "crm-promotion-launched");

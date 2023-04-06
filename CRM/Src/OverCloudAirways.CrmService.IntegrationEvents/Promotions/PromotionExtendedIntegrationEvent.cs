using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.CrmService.Domain.Promotions;

namespace OverCloudAirways.CrmService.IntegrationEvents.Promotions;

public record PromotionExtendedIntegrationEvent(
    PromotionId PromotionId,
    int Months) : IntegrationEvent(PromotionId, "crm-promotion-extended");

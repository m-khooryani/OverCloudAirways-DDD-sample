using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.CrmService.Domain.Promotions.Events;

public record PromotionExtendedDomainEvent(
    PromotionId PromotionId,
    int Months) : DomainEvent(PromotionId);

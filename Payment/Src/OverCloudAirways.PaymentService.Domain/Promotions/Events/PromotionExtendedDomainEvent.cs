using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.PaymentService.Domain.Promotions.Events;

public record PromotionExtendedDomainEvent(
    PromotionId PromotionId,
    int Months) : DomainEvent(PromotionId);

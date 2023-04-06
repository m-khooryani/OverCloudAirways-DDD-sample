using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.Domain.Promotions.Events;

public record PromotionLaunchedDomainEvent(
    PromotionId PromotionId,
    string DiscountCode,
    Percentage DiscountPercentage,
    string? Description,
    BuyerId? BuyerId,
    DateTimeOffset ExpirationDate) : DomainEvent(PromotionId);

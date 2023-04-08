using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.Domain.Orders.Events;

public record DiscountAppliedToOrderDomainEvent(
    OrderId OrderId,
    Percentage AppliedDiscount) : DomainEvent(OrderId);

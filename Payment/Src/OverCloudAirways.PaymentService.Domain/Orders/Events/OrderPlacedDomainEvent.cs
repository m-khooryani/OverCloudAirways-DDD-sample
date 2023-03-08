using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.Domain.Orders.Events;

public record OrderPlacedDomainEvent(
    OrderId OrderId,
    BuyerId BuyerId,
    DateTimeOffset Date,
    IReadOnlyList<PricedOrderItem> OrderItems) : DomainEvent(OrderId);

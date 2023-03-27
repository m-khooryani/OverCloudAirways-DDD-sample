using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.Domain.Orders.Events;

public record OrderCanceledDomainEvent(
    OrderId OrderId,
    BuyerId BuyerId,
    decimal TotalAmount) : DomainEvent(OrderId);

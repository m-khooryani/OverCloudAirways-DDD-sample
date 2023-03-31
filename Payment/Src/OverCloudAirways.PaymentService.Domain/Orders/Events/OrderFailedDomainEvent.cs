using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.Domain.Orders.Events;

public record OrderFailedDomainEvent(
    OrderId OrderId,
    BuyerId BuyerId,
    decimal PaidAmount) : DomainEvent(OrderId);

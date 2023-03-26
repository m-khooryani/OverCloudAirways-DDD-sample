using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.PaymentService.Domain.Orders.Events;

public record OrderConfirmedDomainEvent(OrderId OrderId) : DomainEvent(OrderId);

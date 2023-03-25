using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.PaymentService.Domain.Orders.Events;

public record OrderCanceledDomainEvent(OrderId OrderId) : DomainEvent(OrderId);

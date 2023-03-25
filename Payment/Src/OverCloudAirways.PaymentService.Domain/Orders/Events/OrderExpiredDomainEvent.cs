using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.PaymentService.Domain.Orders.Events;

public record OrderExpiredDomainEvent(OrderId OrderId) : DomainEvent(OrderId);

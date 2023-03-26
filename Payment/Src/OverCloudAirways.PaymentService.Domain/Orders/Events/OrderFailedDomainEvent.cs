using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.PaymentService.Domain.Orders.Events;

public record OrderFailedDomainEvent(OrderId OrderId) : DomainEvent(OrderId);

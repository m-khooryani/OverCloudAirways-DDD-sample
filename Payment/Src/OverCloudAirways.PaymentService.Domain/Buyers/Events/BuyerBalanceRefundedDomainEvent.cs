using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.PaymentService.Domain.Buyers.Events;

public record BuyerBalanceRefundedDomainEvent(
    BuyerId BuyerId,
    decimal Amount) : DomainEvent(BuyerId);
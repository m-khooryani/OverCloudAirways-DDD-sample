using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.CrmService.Domain.Customers;

namespace OverCloudAirways.CrmService.Domain.Purchases.Events;

public record PurchaseMadeDomainEvent(
    PurchaseId PurchaseId,
    CustomerId CustomerId,
    DateTimeOffset Date,
    decimal Amount) : DomainEvent(PurchaseId);

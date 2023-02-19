using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.PaymentService.Domain.Products.Events;

public record ProductUpdatedDomainEvent(
    ProductId ProductId,
    string Name,
    string Description,
    decimal Price) : DomainEvent(ProductId);

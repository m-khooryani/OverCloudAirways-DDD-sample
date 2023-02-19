using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.PaymentService.Domain.Products.Events;

public record ProductCreatedDomainEvent(
    ProductId ProductId,
    string Name,
    string Description,
    decimal Price) : DomainEvent(ProductId);

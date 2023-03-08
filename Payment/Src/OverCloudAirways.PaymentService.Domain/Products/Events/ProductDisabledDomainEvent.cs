using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.PaymentService.Domain.Products.Events;

public record ProductDisabledDomainEvent(
    ProductId ProductId) : DomainEvent(ProductId);

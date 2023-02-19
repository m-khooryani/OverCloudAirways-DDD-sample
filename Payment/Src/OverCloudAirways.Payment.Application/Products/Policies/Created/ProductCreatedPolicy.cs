using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Products.Events;

namespace OverCloudAirways.PaymentService.Application.Products.Policies.Created;

public class ProductCreatedPolicy : DomainEventPolicy<ProductCreatedDomainEvent>
{
    public ProductCreatedPolicy(ProductCreatedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

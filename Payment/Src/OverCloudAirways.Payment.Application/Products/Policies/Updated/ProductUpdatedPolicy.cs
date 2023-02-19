using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Products.Events;

namespace OverCloudAirways.PaymentService.Application.Products.Policies.Updated;

public class ProductUpdatedPolicy : DomainEventPolicy<ProductUpdatedDomainEvent>
{
    public ProductUpdatedPolicy(ProductUpdatedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

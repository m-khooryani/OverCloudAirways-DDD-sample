using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Products.Events;

namespace OverCloudAirways.PaymentService.Application.Products.Policies.Disabled;

public class ProductDisabledPolicy : DomainEventPolicy<ProductDisabledDomainEvent>
{
    public ProductDisabledPolicy(ProductDisabledDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

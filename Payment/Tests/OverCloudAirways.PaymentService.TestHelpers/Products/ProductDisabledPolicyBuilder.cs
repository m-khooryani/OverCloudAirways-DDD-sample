using OverCloudAirways.PaymentService.Application.Products.Policies.Disabled;
using OverCloudAirways.PaymentService.Domain.Products.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Products;

public class ProductDisabledPolicyBuilder
{
    private ProductDisabledDomainEvent _domainEvent = new ProductDisabledDomainEventBuilder().Build();

    public ProductDisabledPolicy Build()
    {
        return new ProductDisabledPolicy(_domainEvent);
    }

    public ProductDisabledPolicyBuilder SetDomainEvent(ProductDisabledDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}

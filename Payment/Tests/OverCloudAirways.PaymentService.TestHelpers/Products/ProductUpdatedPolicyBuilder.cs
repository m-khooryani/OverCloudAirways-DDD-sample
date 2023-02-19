using OverCloudAirways.PaymentService.Application.Products.Policies.Updated;
using OverCloudAirways.PaymentService.Domain.Products.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Products;

public class ProductUpdatedPolicyBuilder
{
    private ProductUpdatedDomainEvent _domainEvent = new ProductUpdatedDomainEventBuilder().Build();

    public ProductUpdatedPolicy Build()
    {
        return new ProductUpdatedPolicy(_domainEvent);
    }

    public ProductUpdatedPolicyBuilder SetDomainEvent(ProductUpdatedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
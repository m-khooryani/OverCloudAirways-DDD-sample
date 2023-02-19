using OverCloudAirways.PaymentService.Application.Products.Policies.Created;
using OverCloudAirways.PaymentService.Domain.Products.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Products;

public class ProductCreatedPolicyBuilder
{
    private ProductCreatedDomainEvent _domainEvent = new ProductCreatedDomainEventBuilder().Build();

    public ProductCreatedPolicy Build()
    {
        return new ProductCreatedPolicy(_domainEvent);
    }

    public ProductCreatedPolicyBuilder SetDomainEvent(ProductCreatedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
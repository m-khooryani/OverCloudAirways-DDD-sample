using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.Domain.Products.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Products;

public class ProductDisabledDomainEventBuilder
{
    private ProductId _productId = ProductId.New();

    public ProductDisabledDomainEvent Build()
    {
        return new ProductDisabledDomainEvent(_productId);
    }

    public ProductDisabledDomainEventBuilder SetProductId(ProductId productId)
    {
        _productId = productId;
        return this;
    }
}

using OverCloudAirways.PaymentService.Application.Products.Commands.Disable;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.TestHelpers.Products;

public class DisableProductCommandBuilder
{
    private ProductId _productId = ProductId.New();

    public DisableProductCommand Build()
    {
        return new DisableProductCommand(_productId);
    }

    public DisableProductCommandBuilder SetProductId(ProductId productId)
    {
        _productId = productId;
        return this;
    }
}
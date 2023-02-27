using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderItemBuilder
{
    private ProductId _productId = ProductId.New();
    private int _quantity = 1;

    public OrderItem Build()
    {
        return OrderItem.Of(_productId, _quantity);
    }

    public OrderItemBuilder SetProductId(ProductId productId)
    {
        _productId = productId; 
        return this;
    }

    public OrderItemBuilder SetQuantity(int quantity)
    {
        _quantity = quantity;
        return this;
    }
}

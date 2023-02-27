using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class PricedOrderItemBuilder
{
    private ProductId _productId = ProductId.New();
    private decimal _unitPrice = 100M;
    private int _quantity = 1;

    public PricedOrderItem Build()
    {
        return PricedOrderItem.Of(_productId, _unitPrice, _quantity);
    }

    public PricedOrderItemBuilder SetProductId(ProductId productId)
    {
        _productId = productId; 
        return this;
    }

    public PricedOrderItemBuilder SetUnitPrice(decimal unitPrice)
    {
        _unitPrice = unitPrice;
        return this;
    }

    public PricedOrderItemBuilder SetQuantity(int quantity)
    {
        _quantity = quantity;
        return this;
    }
}

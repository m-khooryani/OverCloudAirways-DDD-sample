using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.Domain.Orders;

public class PricedOrderItem : ValueObject
{
    public ProductId ProductId { get; }
    public decimal UnitPrice { get; }
    public int Quantity { get; }
    public decimal TotalPrice => Quantity * UnitPrice;

    private PricedOrderItem() 
    {
    }

    private PricedOrderItem(ProductId productId, decimal unitPrice, int quantity)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public static PricedOrderItem Of(ProductId productId, decimal unitPrice, int quantity)
    {
        return new PricedOrderItem(productId, unitPrice, quantity);
    }

    internal static PricedOrderItem Of(OrderItem item, Product product)
    {
        return Of(item.ProductId, product.Price, item.Quantity);
    }
}

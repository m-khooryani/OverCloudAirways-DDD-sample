using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.Domain.Orders;

public class OrderItem : ValueObject
{
    public ProductId ProductId { get; }
    public int Quantity { get; }

    private OrderItem() 
    {
    }

    [JsonConstructor]
    private OrderItem(ProductId productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public static OrderItem Of(ProductId productId, int quantity)
    {
        return new OrderItem(productId, quantity);
    }
}

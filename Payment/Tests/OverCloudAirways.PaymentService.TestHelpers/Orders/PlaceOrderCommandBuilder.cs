using OverCloudAirways.PaymentService.Application.Orders.Commands.Place;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class PlaceOrderCommandBuilder
{
    private OrderId _orderId = OrderId.New();
    private BuyerId _buyerId = BuyerId.New();
    private List<OrderItem> _items = new List<OrderItem>()
    {
        new OrderItemBuilder().Build()
    };

    public PlaceOrderCommand Build()
    {
        return new PlaceOrderCommand(_orderId, _buyerId, _items);
    }

    public PlaceOrderCommandBuilder SetOrderId(OrderId orderId)
    {
        _orderId = orderId;
        return this;
    }

    public PlaceOrderCommandBuilder SetBuyerId(BuyerId buyerId)
    {
        _buyerId = buyerId;
        return this;
    }

    public PlaceOrderCommandBuilder ClearOrderItems()
    {
        _items.Clear();
        return this;
    }

    public PlaceOrderCommandBuilder AddOrderItem(OrderItem item)
    {
        _items.Add(item);
        return this;
    }
}

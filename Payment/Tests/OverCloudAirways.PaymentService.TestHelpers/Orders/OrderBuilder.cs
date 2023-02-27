using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderBuilder
{
    private IAggregateRepository _repository;
    private OrderId _orderId = OrderId.New();
    private BuyerId _buyerId = BuyerId.New();
    private List<OrderItem> _items = new List<OrderItem>()
    {
        new OrderItemBuilder().Build()
    };

    public async Task<Order> BuildAsync()
    {
        return await Order.PlaceAsync(_repository, _orderId, _buyerId, _items);
    }

    public OrderBuilder SetAggregateRepository(IAggregateRepository repository)
    {
        _repository = repository;
        return this;
    }

    public OrderBuilder SetOrderId(OrderId orderId)
    {
        _orderId = orderId;
        return this;
    }

    public OrderBuilder SetBuyerId(BuyerId buyerId)
    {
        _buyerId = buyerId;
        return this;
    }

    public OrderBuilder ClearItems()
    {
        _items.Clear(); 
        return this;
    }

    public OrderBuilder AddOrderItem(OrderItem item)
    {
        _items.Add(item);
        return this;
    }
}

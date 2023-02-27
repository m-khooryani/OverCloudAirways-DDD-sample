using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderPlacedDomainEventBuilder
{
    private OrderId _orderId = OrderId.New();
    private BuyerId _buyerId = BuyerId.New();
    private DateTimeOffset _date = Clock.Now;
    private List<PricedOrderItem> _items = new List<PricedOrderItem>()
    {
        new PricedOrderItemBuilder().Build()
    };

    public OrderPlacedDomainEvent Build()
    {
        return new OrderPlacedDomainEvent(_orderId, _buyerId, _date, _items);
    }

    public OrderPlacedDomainEventBuilder SetOrderId(OrderId orderId)
    {
        _orderId = orderId;
        return this;
    }

    public OrderPlacedDomainEventBuilder SetBuyerId(BuyerId buyerId)
    {
        _buyerId = buyerId;
        return this;
    }

    public OrderPlacedDomainEventBuilder SetDate(DateTimeOffset date)
    {
        _date = date;
        return this;
    }

    public OrderPlacedDomainEventBuilder AddOrderItem(PricedOrderItem item)
    {
        _items.Add(item);
        return this;
    }
}

using System.Collections.ObjectModel;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Orders.Events;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.Domain.Orders;

public class Order : AggregateRoot<OrderId>
{
    private List<PricedOrderItem> _orderItems;
    public BuyerId BuyerId { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public decimal TotalAmount => _orderItems.Sum(oi => oi.TotalPrice);
    public IReadOnlyCollection<PricedOrderItem> OrderItems => _orderItems.AsReadOnly();

    private Order()
    {
    }

    public static async Task<Order> PlaceAsync(
        IAggregateRepository repository,
        OrderId orderId,
        BuyerId buyerId,
        IReadOnlyList<OrderItem> orderItems)
    {
        var pricedOrderItems = await GetPricedOrderItems(repository, orderItems);

        var order = new Order();
        var @event = new OrderPlacedDomainEvent(orderId, buyerId, Clock.Now, pricedOrderItems);
        order.Apply(@event);

        return order;
    }

    private static async Task<ReadOnlyCollection<PricedOrderItem>> GetPricedOrderItems(IAggregateRepository repository, IReadOnlyList<OrderItem> orderItems)
    {
        var pricedOrderItems = new List<PricedOrderItem>();
        foreach (var orderItem in orderItems)
        {
            // TIP: Alternatively a domain service can be used for DB query optimization
            var product = await repository.LoadAsync<Product, ProductId>(orderItem.ProductId);
            pricedOrderItems.Add(PricedOrderItem.Of(orderItem, product));
        }

        return pricedOrderItems.AsReadOnly();
    }

    protected void When(OrderPlacedDomainEvent @event)
    {
        Id = @event.OrderId;
        BuyerId = @event.BuyerId;
        Date = @event.Date;
        _orderItems = new List<PricedOrderItem>(@event.OrderItems);
    }
}

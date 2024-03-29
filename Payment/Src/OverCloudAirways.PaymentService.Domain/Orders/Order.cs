﻿using System.Collections.ObjectModel;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Orders.Events;
using OverCloudAirways.PaymentService.Domain.Orders.Rules;
using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.Domain.Orders;

public class Order : AggregateRoot<OrderId>
{
    private List<PricedOrderItem> _orderItems;
    public BuyerId BuyerId { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public OrderStatus Status { get; private set; }
    public Percentage? AppliedDiscount { get; private set; }
    public IReadOnlyCollection<PricedOrderItem> OrderItems => _orderItems.AsReadOnly();
    public decimal TotalAmount =>
         _orderItems.Sum(oi => oi.TotalPrice) * (100M - (AppliedDiscount?.Value ?? 0)) / 100M;

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

    public async Task ExpireAsync()
    {
        await CheckRuleAsync(new OnlyPendingOrdersCanBeModifiedRule(Status));

        var @event = new OrderExpiredDomainEvent(Id);
        Apply(@event);
    }

    public async Task CancelAsync()
    {
        await CheckRuleAsync(new OnlyPendingOrdersCanBeModifiedRule(Status));

        var @event = new OrderCanceledDomainEvent(Id, BuyerId, TotalAmount);
        Apply(@event);
    }

    public void Confirm(Invoice invoice)
    {
        var @event = ConfirmationStrategy(invoice);
        Apply(@event);
    }

    private DomainEvent ConfirmationStrategy(Invoice invoice)
    {
        if (invoice.TotalAmount != TotalAmount)
        {
            return new OrderFailedDomainEvent(Id, BuyerId, invoice.TotalAmount);
        }
        return new OrderConfirmedDomainEvent(Id, invoice.Id);
    }

    private static async Task<ReadOnlyCollection<PricedOrderItem>> GetPricedOrderItems(IAggregateRepository repository, IReadOnlyList<OrderItem> orderItems)
    {
        // Tip: Using a domain service can be considered to handle the
        // querying of the products for the order items, rather than the repository.
        // This can help to optimize the queries and reduce the number of round trips to the database.
        var pricedOrderItems = new List<PricedOrderItem>();
        foreach (var orderItem in orderItems)
        {
            var product = await repository.LoadAsync<Product, ProductId>(orderItem.ProductId);
            pricedOrderItems.Add(PricedOrderItem.Of(orderItem, product));
        }

        return pricedOrderItems.AsReadOnly();
    }

    public async Task ApplyDiscountAsync(Promotion promotion)
    {
        await CheckRuleAsync(new DiscountCanBeAppliedForOrderWithValidBuyerRule(BuyerId, promotion));
        await CheckRuleAsync(new DiscountCanBeAppliedBeforeExpirationRule(promotion));

        var @event = new DiscountAppliedToOrderDomainEvent(Id, promotion.DiscountPercentage);
        Apply(@event);

        await Task.CompletedTask;
    }

    protected void When(OrderPlacedDomainEvent @event)
    {
        Id = @event.OrderId;
        BuyerId = @event.BuyerId;
        Date = @event.Date;
        Status = OrderStatus.Pending;
        _orderItems = new List<PricedOrderItem>(@event.OrderItems);
    }

    protected void When(OrderExpiredDomainEvent _)
    {
        Status = OrderStatus.Expired;
    }

    protected void When(OrderCanceledDomainEvent _)
    {
        Status = OrderStatus.Canceled;
    }

    protected void When(OrderFailedDomainEvent _)
    {
        Status = OrderStatus.Failed;
    }

    protected void When(OrderConfirmedDomainEvent _)
    {
        Status = OrderStatus.Confirmed;
    }

    protected void When(DiscountAppliedToOrderDomainEvent @event)
    {
        AppliedDiscount = @event.AppliedDiscount;
    }
}

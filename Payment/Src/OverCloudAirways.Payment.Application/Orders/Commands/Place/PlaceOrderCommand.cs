using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.Place;

public record PlaceOrderCommand(
    OrderId OrderId,
    BuyerId BuyerId,
    IReadOnlyList<OrderItem> OrderItems) : Command;

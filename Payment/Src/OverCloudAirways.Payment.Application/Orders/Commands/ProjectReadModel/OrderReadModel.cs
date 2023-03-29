using OverCloudAirways.BuildingBlocks.Application.Models;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;

internal record OrderReadModel(
    Guid Id,
    string BuyerFirstName,
    string BuyerLastName,
    DateTimeOffset Date,
    decimal TotalAmount,
    OrderStatus Status,
    IReadOnlyList<OrderItemReadModel> OrderItems) : ReadModel(Id.ToString(), Id.ToString());

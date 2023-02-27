using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;

internal record OrderReadModel(
    Guid Id,
    string BuyerFirstName,
    string BuyerLastName,
    DateTimeOffset Date,
    decimal TotalAmount,
    IReadOnlyList<OrderItemReadModel> OrderItems) : ReadModel(Id.ToString(), Id.ToString());

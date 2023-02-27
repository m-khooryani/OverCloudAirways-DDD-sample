using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.PaymentService.Application.Orders.Queries.GetInfo;

public record OrderDto(
    Guid Id,
    string BuyerFirstName,
    string BuyerLastName,
    DateTimeOffset Date,
    decimal TotalAmount,
    IReadOnlyList<OrderItemDto> OrderItems) : ReadModel(Id.ToString(), Id.ToString());

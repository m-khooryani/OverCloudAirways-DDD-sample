using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Application.Orders.Queries.GetInfo;

public record OrderDto(
    Guid Id,
    string BuyerFirstName,
    string BuyerLastName,
    DateTimeOffset Date,
    decimal TotalAmount,
    OrderStatus Status,
    IReadOnlyList<OrderItemDto> OrderItems);

namespace OverCloudAirways.PaymentService.Application.Orders.Queries.GetInfo;

public record OrderItemDto(
    string ProductName,
    decimal ProductPrice,
    int Quantity,
    decimal TotalPrice);

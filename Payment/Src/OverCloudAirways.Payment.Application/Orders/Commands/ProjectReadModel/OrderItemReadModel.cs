namespace OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;

internal record OrderItemReadModel(
    Guid ProductId,
    string ProductName,
    decimal ProductPrice,
    int Quantity,
    decimal TotalPrice);
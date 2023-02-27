namespace OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;

internal record OrderItemReadModel(
    string ProductName,
    decimal ProductPrice,
    int Quantity,
    decimal TotalPrice);
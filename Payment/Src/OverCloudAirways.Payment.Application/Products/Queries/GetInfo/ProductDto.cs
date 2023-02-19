namespace OverCloudAirways.PaymentService.Application.Products.Queries.GetInfo;

public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    bool IsEnabled);

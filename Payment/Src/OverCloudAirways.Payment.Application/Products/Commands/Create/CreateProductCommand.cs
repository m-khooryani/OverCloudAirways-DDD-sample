using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.Application.Products.Commands.Create;

public record CreateProductCommand(
    ProductId ProductId,
    string Name,
    string Description,
    decimal Price) : Command;

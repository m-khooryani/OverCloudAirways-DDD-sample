using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.Application.Products.Commands.ProjectReadModel;

public record ProjectProductReadModelCommand(ProductId ProductId) : Command;

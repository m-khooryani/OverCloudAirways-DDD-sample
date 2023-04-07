using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.PaymentService.Application.Buyers.Commands.ProjectReadModel;

internal record ProductReadModel(
    Guid ProductId,
    string Name,
    string Description,
    decimal Price,
    bool IsEnabled) : ReadModel(ProductId.ToString(), ProductId.ToString());

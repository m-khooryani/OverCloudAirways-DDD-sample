using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.PaymentService.Application.Buyers.Commands.ProjectReadModel;

internal record ProductReadModel(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    bool IsEnabled) : ReadModel(Id.ToString(), Id.ToString());

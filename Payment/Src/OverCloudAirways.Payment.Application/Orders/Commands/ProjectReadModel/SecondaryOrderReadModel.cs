using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;

internal record SecondaryOrderReadModel(
    Guid OrderId,
    Guid ProductId) : ReadModel(OrderId.ToString(), ProductId.ToString());

using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;

public record ProjectOrderReadModelCommand(OrderId OrderId) : Command;

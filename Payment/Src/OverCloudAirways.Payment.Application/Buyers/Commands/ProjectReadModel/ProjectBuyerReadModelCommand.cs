using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.Application.Buyers.Commands.ProjectReadModel;

public record ProjectBuyerReadModelCommand(BuyerId BuyerId) : Command;

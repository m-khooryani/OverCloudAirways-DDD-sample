using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.Application.Promotions.Commands.ProjectReadModel;

public record ProjectPromotionReadModelCommand(PromotionId PromotionId) : Command;

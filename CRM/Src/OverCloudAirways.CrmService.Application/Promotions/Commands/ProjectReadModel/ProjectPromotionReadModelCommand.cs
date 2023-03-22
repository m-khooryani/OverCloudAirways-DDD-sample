using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.CrmService.Domain.Promotions;

namespace OverCloudAirways.CrmService.Application.Promotions.Commands.ProjectReadModel;

public record ProjectPromotionReadModelCommand(PromotionId PromotionId) : Command;

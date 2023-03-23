using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.CrmService.Domain.Promotions;

namespace OverCloudAirways.CrmService.Application.Promotions.Commands.Extend;

public record ExtendPromotionCommand(
    PromotionId PromotionId,
    int Months) : Command;

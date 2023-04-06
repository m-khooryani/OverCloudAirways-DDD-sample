using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.Application.Promotions.Commands.Extend;

public record ExtendPromotionCommand(
    PromotionId PromotionId,
    int Months) : Command;

using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.Application.Promotions.Commands.Launch;

public record LaunchPromotionCommand(
    PromotionId PromotionId,
    string DiscountCode,
    Percentage DiscountPercentage,
    string? Description,
    BuyerId? BuyerId,
    DateTimeOffset ExpirationDate) : Command;

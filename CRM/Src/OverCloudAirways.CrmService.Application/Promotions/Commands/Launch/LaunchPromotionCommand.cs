using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;
using OverCloudAirways.CrmService.Domain.Promotions;

namespace OverCloudAirways.CrmService.Application.Promotions.Commands.Launch;

public record LaunchPromotionCommand(
    PromotionId PromotionId,
    Percentage DiscountPercentage,
    string? Description,
    CustomerId? CustomerId) : Command;

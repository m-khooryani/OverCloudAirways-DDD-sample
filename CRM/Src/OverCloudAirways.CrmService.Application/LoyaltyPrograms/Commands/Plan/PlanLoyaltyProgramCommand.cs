using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.Plan;

public record PlanLoyaltyProgramCommand(
    LoyaltyProgramId LoyaltyProgramId,
    string Name,
    decimal PurchaseRequirements,
    Percentage DiscountPercentage) : Command;

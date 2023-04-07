using OverCloudAirways.BuildingBlocks.Application.Models;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.ProjectReadModel;

internal record LoyaltyProgramReadModel(
    Guid LoyaltyProgramId,
    string Name,
    decimal PurchaseRequirements,
    Percentage DiscountPercentage,
    bool IsSuspended) : ReadModel(LoyaltyProgramId.ToString(), "LoyaltyPrograms");
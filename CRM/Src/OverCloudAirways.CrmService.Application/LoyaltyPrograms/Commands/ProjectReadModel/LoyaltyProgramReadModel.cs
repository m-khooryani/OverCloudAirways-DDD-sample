using OverCloudAirways.BuildingBlocks.Application.Models;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.ProjectReadModel;

internal record LoyaltyProgramReadModel(
    Guid Id,
    string Name,
    decimal PurchaseRequirements,
    Percentage DiscountPercentage) : ReadModel(Id.ToString(), Id.ToString());
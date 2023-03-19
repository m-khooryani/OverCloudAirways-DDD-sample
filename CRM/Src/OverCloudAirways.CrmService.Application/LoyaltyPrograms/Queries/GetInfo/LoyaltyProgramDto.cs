using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Queries.GetInfo;

public record LoyaltyProgramDto(
    Guid Id,
    string Name,
    decimal PurchaseRequirements,
    Percentage DiscountPercentage);
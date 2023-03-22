using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.Promotions.Queries;

public record PromotionDto(
    Guid Id,
    string DiscountCode,
    Percentage DiscountPercentage,
    string? Description,
    DateTimeOffset ExpirationDate,
    string? CustomerFirstName,
    string? CustomerLastName);
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.Application.Promotions.Queries;

public record PromotionDto(
    Guid Id,
    string DiscountCode,
    Percentage DiscountPercentage,
    string? Description,
    DateTimeOffset ExpirationDate,
    string? CustomerFirstName,
    string? CustomerLastName);
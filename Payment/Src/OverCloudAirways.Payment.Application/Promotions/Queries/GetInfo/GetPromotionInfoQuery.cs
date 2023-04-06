using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.PaymentService.Application.Promotions.Queries.GetInfo;

public record GetPromotionInfoQuery(string DiscountCode) : Query<PromotionDto>;

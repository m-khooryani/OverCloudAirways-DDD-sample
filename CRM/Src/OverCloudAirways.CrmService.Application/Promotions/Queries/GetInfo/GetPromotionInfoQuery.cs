using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.CrmService.Application.Promotions.Queries.GetInfo;

public record GetPromotionInfoQuery(string DiscountCode) : Query<PromotionDto>;

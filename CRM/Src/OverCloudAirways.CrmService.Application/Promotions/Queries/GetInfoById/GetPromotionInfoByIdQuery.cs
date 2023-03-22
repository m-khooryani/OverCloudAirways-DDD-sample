using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.CrmService.Application.Promotions.Queries.GetInfoById;

public record GetPromotionInfoByIdQuery(Guid PromotionId) : Query<PromotionDto>;

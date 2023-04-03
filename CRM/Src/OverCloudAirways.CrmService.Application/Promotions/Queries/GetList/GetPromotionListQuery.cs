using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.CrmService.Application.Promotions.Queries.GetList;

public record GetPromotionListQuery(
    int PageNumber,
    int PageSize) : PageableQuery<PromotionDto>(PageNumber, PageSize);

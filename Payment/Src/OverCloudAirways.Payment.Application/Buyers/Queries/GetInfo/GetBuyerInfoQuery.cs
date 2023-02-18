using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.PaymentService.Application.Buyers.Queries.GetInfo;

public record GetBuyerInfoQuery(Guid BuyerId) : Query<BuyerDto>;

using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.PaymentService.Application.Orders.Queries.GetInfo;

public record GetOrderInfoQuery(Guid OrderId) : Query<OrderDto>;

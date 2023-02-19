using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.PaymentService.Application.Products.Queries.GetInfo;

public record GetProductInfoQuery(Guid ProductId) : Query<ProductDto>;

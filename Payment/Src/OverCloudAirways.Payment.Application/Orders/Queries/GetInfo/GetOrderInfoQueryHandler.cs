using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.PaymentService.Application.Orders.Queries.GetInfo;

internal class GetOrderInfoQueryHandler : QueryHandler<GetOrderInfoQuery, OrderDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetOrderInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<OrderDto> HandleAsync(GetOrderInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    orders.Id             AS {nameof(OrderDto.Id)}, 
                    orders.BuyerFirstName AS {nameof(OrderDto.BuyerFirstName)}, 
                    orders.BuyerLastName  AS {nameof(OrderDto.BuyerLastName)}, 
                    orders.Date           AS {nameof(OrderDto.Date)}, 
                    orders.TotalAmount    AS {nameof(OrderDto.TotalAmount)},
                    orders.Status         AS {nameof(OrderDto.Status)},
                    orders.OrderItems     AS {nameof(OrderDto.OrderItems)}
                    FROM orders 
                    WHERE 
                    orders.id = @orderId AND 
                    orders.partitionKey = @orderId 
        ";
        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@orderId", query.OrderId);
        var order = await _cosmosManager.QuerySingleAsync<OrderDto>(ContainersConstants.ReadModels, queryDefinition);

        return order;
    }
}

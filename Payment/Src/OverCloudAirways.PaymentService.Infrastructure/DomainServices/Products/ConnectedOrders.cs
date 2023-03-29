using System.Collections.ObjectModel;
using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.Infrastructure.DomainServices.Products;

internal class ConnectedOrders : IConnectedOrders
{
    private readonly ICosmosManager _cosmosManager;

    public ConnectedOrders(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public async Task<ReadOnlyCollection<OrderId>> GetConnectedOrderIds(ProductId productId)
    {
        var sql = @$"
                    SELECT 
                    orders.OrderId AS {nameof(OrderModel.Id)}
                    FROM orders
                    WHERE 
                    orders.partitionKey = @productId 
        ";
        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@productId", productId);
        var orders = await _cosmosManager.AsListAsync<OrderModel>("readmodels", queryDefinition);

        return orders
            .Where(x => x.Id is not null)
            .Select(x => x.Id)
            .ToList()
            .AsReadOnly();
    }

    private class OrderModel
    {
        public OrderId Id { get; set; }
    }
}

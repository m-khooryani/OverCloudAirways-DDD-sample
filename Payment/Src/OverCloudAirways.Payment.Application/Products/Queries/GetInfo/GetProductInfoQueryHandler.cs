using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.PaymentService.Application.Products.Queries.GetInfo;

internal class GetProductInfoQueryHandler : QueryHandler<GetProductInfoQuery, ProductDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetProductInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<ProductDto> HandleAsync(GetProductInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    product.ProductId   AS {nameof(ProductDto.Id)}, 
                    product.Name        AS {nameof(ProductDto.Name)}, 
                    product.Description AS {nameof(ProductDto.Description)}, 
                    product.Price       AS {nameof(ProductDto.Price)}, 
                    product.IsEnabled   AS {nameof(ProductDto.IsEnabled)}
                    FROM product 
                    WHERE 
                    product.id = @productId AND 
                    product.partitionKey = @productId 
        ";
        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@productId", query.ProductId);
        var product = await _cosmosManager.QuerySingleAsync<ProductDto>(ContainersConstants.ReadModels, queryDefinition);

        return product;
    }
}
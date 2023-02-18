using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.PaymentService.Application.Buyers.Queries.GetInfo;

internal class GetBuyerInfoQueryHandler : QueryHandler<GetBuyerInfoQuery, BuyerDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetBuyerInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<BuyerDto> HandleAsync(GetBuyerInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    buyer.Id          AS {nameof(BuyerDto.Id)}, 
                    buyer.FirstName   AS {nameof(BuyerDto.FirstName)}, 
                    buyer.LastName    AS {nameof(BuyerDto.LastName)}, 
                    buyer.Email       AS {nameof(BuyerDto.Email)}, 
                    buyer.PhoneNumber AS {nameof(BuyerDto.PhoneNumber)}
                    FROM buyer 
                    WHERE 
                    buyer.id = @buyerId AND 
                    buyer.partitionKey = @buyerId ";

        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@buyerId", query.BuyerId);
        var buyer = await _cosmosManager.QuerySingleAsync<BuyerDto>(ContainersConstants.ReadModels, queryDefinition);

        return buyer;
    }
}
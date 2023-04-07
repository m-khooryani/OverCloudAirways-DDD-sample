using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.BookingService.Application.Customers.Queries.GetInfo;

internal class GetCustomerInfoQueryHandler : QueryHandler<GetCustomerInfoQuery, CustomerDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetCustomerInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<CustomerDto> HandleAsync(GetCustomerInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    customer.CustomerId  AS {nameof(CustomerDto.Id)}, 
                    customer.FirstName   AS {nameof(CustomerDto.FirstName)}, 
                    customer.LastName    AS {nameof(CustomerDto.LastName)}, 
                    customer.Email       AS {nameof(CustomerDto.Email)}, 
                    customer.DateOfBirth AS {nameof(CustomerDto.DateOfBirth)},
                    customer.PhoneNumber AS {nameof(CustomerDto.PhoneNumber)},
                    customer.Address     AS {nameof(CustomerDto.Address)}
                    FROM customer 
                    WHERE 
                    customer.id = @customer AND 
                    customer.partitionKey = @customer ";

        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@customer", query.CustomerId);
        var customer = await _cosmosManager.QuerySingleAsync<CustomerDto>(ContainersConstants.ReadModels, queryDefinition);

        return customer;
    }
}

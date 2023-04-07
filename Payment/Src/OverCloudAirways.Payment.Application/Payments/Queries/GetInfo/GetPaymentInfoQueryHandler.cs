using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.PaymentService.Application.Payments.Queries.GetInfo;

internal class GetPaymentInfoQueryHandler : QueryHandler<GetPaymentInfoQuery, PaymentDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetPaymentInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<PaymentDto> HandleAsync(GetPaymentInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    payment.PaymentId       AS {nameof(PaymentDto.Id)}, 
                    payment.Amount          AS {nameof(PaymentDto.Amount)}, 
                    payment.InvoiceAmount   AS {nameof(PaymentDto.InvoiceAmount)},
                    payment.Method          AS {nameof(PaymentDto.Method)},
                    payment.ReferenceNumber AS {nameof(PaymentDto.ReferenceNumber)}
                    FROM payment 
                    WHERE 
                    payment.id = @paymentId AND 
                    payment.partitionKey = @paymentId 
        ";
        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@paymentId", query.PaymentId);
        var payment = await _cosmosManager.QuerySingleAsync<PaymentDto>(ContainersConstants.ReadModels, queryDefinition);

        return payment;
    }
}
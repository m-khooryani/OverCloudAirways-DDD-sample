using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.BookingService.Application.Tickets.Queries.GetInfo;

internal class GetTicketInfoQueryHandler : QueryHandler<GetTicketInfoQuery, TicketDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetTicketInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<TicketDto> HandleAsync(GetTicketInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    ticket.TicketId               AS {nameof(TicketDto.Id)}, 
                    ticket.FlightNumber           AS {nameof(TicketDto.FlightNumber)}, 
                    ticket.DepartureAirportCode   AS {nameof(TicketDto.DepartureAirportCode)}, 
                    ticket.DestinationAirportCode AS {nameof(TicketDto.DestinationAirportCode)}, 
                    ticket.FlightDepartureTime    AS {nameof(TicketDto.FlightDepartureTime)},
                    ticket.FlightArrivalTime      AS {nameof(TicketDto.FlightArrivalTime)},
                    ticket.CustomerFirstName      AS {nameof(TicketDto.CustomerFirstName)},
                    ticket.CustomerLastName       AS {nameof(TicketDto.CustomerLastName)}
                    FROM ticket 
                    WHERE 
                    ticket.id = @ticketId AND 
                    ticket.partitionKey = @ticketId ";

        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@ticketId", query.TicketId);
        var ticket = await _cosmosManager.QuerySingleAsync<TicketDto>(ContainersConstants.ReadModels, queryDefinition);

        return ticket;
    }
}
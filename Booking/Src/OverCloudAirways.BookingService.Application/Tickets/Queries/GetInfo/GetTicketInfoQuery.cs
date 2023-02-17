using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.BookingService.Application.Tickets.Queries.GetInfo;

public record GetTicketInfoQuery(Guid TicketId) : Query<TicketDto>;

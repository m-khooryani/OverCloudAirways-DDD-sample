using OverCloudAirways.BookingService.Domain.Tickets;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Tickets.Commands.ProjectReadModel;

public record ProjectTicketReadModelCommand(TicketId TicketId) : Command;

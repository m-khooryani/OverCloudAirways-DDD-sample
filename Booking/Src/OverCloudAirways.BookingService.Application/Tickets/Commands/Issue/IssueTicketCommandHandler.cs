using OverCloudAirways.BookingService.Domain.Tickets;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Tickets.Commands.Issue;

internal class IssueTicketCommandHandler : CommandHandler<IssueTicketCommand>
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly ITicketSeatNumberGeneratorService _seatNumberGenerator;

    public IssueTicketCommandHandler(
        IAggregateRepository aggregateRepository, 
        ITicketSeatNumberGeneratorService seatNumberGenerator)
    {
        _aggregateRepository = aggregateRepository;
        _seatNumberGenerator = seatNumberGenerator;
    }

    public override async Task HandleAsync(IssueTicketCommand command, CancellationToken cancellationToken)
    {
        var ticket = await Ticket.IssueAsync(_aggregateRepository,
            _seatNumberGenerator,
            command.TicketId,
            command.FlightId,
            command.CustomerId);

        _aggregateRepository.Add(ticket);
    }
}
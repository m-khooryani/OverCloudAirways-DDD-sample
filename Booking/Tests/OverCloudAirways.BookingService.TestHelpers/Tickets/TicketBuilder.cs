using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Tickets;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.TestHelpers.Tickets;

public class TicketBuilder
{
    private IAggregateRepository _aggregateRepository;
    private ITicketSeatNumberGeneratorService _ticketSeatNumberGenerator;
    private TicketId _ticketId = TicketId.New();
    private FlightId _flightId = FlightId.New();
    private CustomerId _customerId = CustomerId.New();

    public async Task<Ticket> BuildAsync()
    {
        return await Ticket.IssueAsync(_aggregateRepository, _ticketSeatNumberGenerator, _ticketId, _flightId, _customerId);
    }

    public TicketBuilder SetAggregateRepository(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
        return this;
    }

    public TicketBuilder SetTicketSeatNumberGeneratorService(ITicketSeatNumberGeneratorService ticketSeatNumberGenerator)
    {
        _ticketSeatNumberGenerator = ticketSeatNumberGenerator;
        return this;
    }

    public TicketBuilder SetTicketId(TicketId ticketId)
    {
        _ticketId = ticketId;
        return this;
    }

    public TicketBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public TicketBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }
}

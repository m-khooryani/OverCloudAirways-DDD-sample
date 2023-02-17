using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Domain.Tickets.Rules;

internal class TicketMustBeIssuedOnlyForFlightsInTheSystemRule : IBusinessRule
{
    private readonly FlightId _flightId;
    private readonly IAggregateRepository _aggregateRepository;

    public TicketMustBeIssuedOnlyForFlightsInTheSystemRule(
        FlightId flightId,
        IAggregateRepository aggregateRepository)
    {
        _flightId = flightId;
        _aggregateRepository = aggregateRepository;
    }

    public string TranslationKey => "Ticket_Must_Be_Issued_Only_For_Flights_In_The_System";

    public async Task<bool> IsFollowedAsync()
    {
        var flight = await _aggregateRepository.LoadAsync<Flight, FlightId>(_flightId);

        return flight is not null;
    }
}

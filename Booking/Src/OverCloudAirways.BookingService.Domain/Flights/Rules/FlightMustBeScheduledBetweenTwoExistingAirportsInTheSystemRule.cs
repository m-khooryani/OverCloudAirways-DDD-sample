using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Domain.Flights.Rules;

internal class FlightMustBeScheduledBetweenTwoExistingAirportsInTheSystemRule : IBusinessRule
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly AirportId _departureAirportId;
    private readonly AirportId _destinationAirportId;

    public FlightMustBeScheduledBetweenTwoExistingAirportsInTheSystemRule(
        IAggregateRepository aggregateRepository,
        AirportId departureAirportId,
        AirportId destinationAirportId)
    {
        _aggregateRepository = aggregateRepository;
        _departureAirportId = departureAirportId;
        _destinationAirportId = destinationAirportId;
    }

    public string TranslationKey => "Flight_Must_Be_Scheduled_Between_Two_Existing_Airports_In_The_System";

    public async Task<bool> IsFollowedAsync()
    {
        var departureAirport = await _aggregateRepository.LoadAsync<Airport, AirportId>(_departureAirportId);
        var destinationAirport = await _aggregateRepository.LoadAsync<Airport, AirportId>(_destinationAirportId);

        return 
            departureAirport is not null &&
            destinationAirport is not null;
    }
}

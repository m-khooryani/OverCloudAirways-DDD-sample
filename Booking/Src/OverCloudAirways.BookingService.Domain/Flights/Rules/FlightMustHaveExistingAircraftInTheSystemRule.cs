using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Domain.Flights.Rules;

internal class FlightMustHaveExistingAircraftInTheSystemRule : IBusinessRule
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly AircraftId _aircraftId;

    public FlightMustHaveExistingAircraftInTheSystemRule(
        IAggregateRepository aggregateRepository,
        AircraftId aircraftId)
    {
        _aggregateRepository = aggregateRepository;
        _aircraftId = aircraftId;
    }

    public string TranslationKey => "Flight_Must_Have_Existing_Aircraft_In_The_System";

    public async Task<bool> IsFollowedAsync()
    {
        var aircraft = await _aggregateRepository.LoadAsync<Aircraft, AircraftId>(_aircraftId);

        return aircraft is not null;
    }
}

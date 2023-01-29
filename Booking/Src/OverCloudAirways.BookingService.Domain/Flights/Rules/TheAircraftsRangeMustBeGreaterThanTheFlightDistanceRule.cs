using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Domain.Flights.Rules;

internal class TheAircraftsRangeMustBeGreaterThanTheFlightDistanceRule : IBusinessRule
{
    private readonly int _distance;
    private readonly AircraftId _aircraftId;
    private readonly IAggregateRepository _aggregateRepository;

    public TheAircraftsRangeMustBeGreaterThanTheFlightDistanceRule(
        int distance,
        AircraftId aircraftId,
        IAggregateRepository aggregateRepository)
    {
        _distance = distance;
        _aircraftId = aircraftId;
        _aggregateRepository = aggregateRepository;
    }

    public string TranslationKey => "The_Aircrafts_Range_Must_Be_Greater_Than_The_Flight_Distance";

    public async Task<bool> IsFollowedAsync()
    {
        var aircraft = await _aggregateRepository.LoadAsync<Aircraft, AircraftId>(_aircraftId);
        return aircraft.Range > _distance;
    }
}

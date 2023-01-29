using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightBuilder
{
    private IAggregateRepository _aggregateRepository = null;
    private FlightId _flightId = FlightId.New();
    private string _number = "AA123";
    private AirportId _departureAirportId = AirportId.New();
    private AirportId _destinationAirportId = AirportId.New();
    private DateTimeOffset _departureTime = Clock.Now.AddDays(7);
    private DateTimeOffset _arrivalTime = Clock.Now.AddDays(8);
    private string _route = "New York - Los Angeles";
    private int _distance = 12_000;
    private AircraftId _aircraftId = AircraftId.New();
    private int _availableSeats = 300;
    private double _maximumLuggageWeight = 35.0;

    public async Task<Flight> BuildAsync()
    {
        return await Flight.ScheduleAsync(
            _aggregateRepository,
            _flightId,
            _number,
            _departureAirportId,
            _destinationAirportId,
            _departureTime,
            _arrivalTime,
            _route,
            _distance,
            _aircraftId,
            _availableSeats,
            _maximumLuggageWeight);
    }

    public FlightBuilder SetAggregateRepository(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
        return this;
    }

    public FlightBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public FlightBuilder SetNumber(string number)
    {
        _number = number;
        return this;
    }

    public FlightBuilder SetDepartureAirportId(AirportId departureAirportId)
    {
        _departureAirportId = departureAirportId;
        return this;
    }

    public FlightBuilder SetDestinationAirportId(AirportId destinationAirportId)
    {
        _destinationAirportId = destinationAirportId;
        return this;
    }

    public FlightBuilder SetDepartureTime(DateTimeOffset departureTime)
    {
        _departureTime = departureTime;
        return this;
    }

    public FlightBuilder SetArrivalTime(DateTimeOffset arrivalTime)
    {
        _arrivalTime = arrivalTime;
        return this;
    }

    public FlightBuilder SetRoute(string route)
    {
        _route = route;
        return this;
    }

    public FlightBuilder SetDistance(int distance)
    {
        _distance = distance;
        return this;
    }

    public FlightBuilder SetAircraftId(AircraftId aircraftId)
    {
        _aircraftId = aircraftId;
        return this;
    }

    public FlightBuilder SetAvailableSeats(int availableSeats)
    {
        _availableSeats = availableSeats;
        return this;
    }

    public FlightBuilder SetMaximumLuggageWeight(double maximumLuggageWeight)
    {
        _maximumLuggageWeight = maximumLuggageWeight;
        return this;
    }
}

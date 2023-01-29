using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightScheduledDomainEventBuilder
{
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
    private int _bookedSeats = 0;
    private double _maximumLuggageWeight = 35.0;

    public FlightScheduledDomainEvent Build()
    {
        return new FlightScheduledDomainEvent(
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
            _bookedSeats, 
            _maximumLuggageWeight);
    }

    public FlightScheduledDomainEventBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public FlightScheduledDomainEventBuilder SetNumber(string number)
    {
        _number = number;
        return this;
    }

    public FlightScheduledDomainEventBuilder SetDepartureAirportId(AirportId departureAirportId)
    {
        _departureAirportId = departureAirportId;
        return this;
    }

    public FlightScheduledDomainEventBuilder SetDestinationAirportId(AirportId destinationAirportId)
    {
        _destinationAirportId = destinationAirportId;
        return this;
    }

    public FlightScheduledDomainEventBuilder SetDepartureTime(DateTimeOffset departureTime)
    {
        _departureTime = departureTime;
        return this;
    }

    public FlightScheduledDomainEventBuilder SetArrivalTime(DateTimeOffset arrivalTime)
    {
        _arrivalTime = arrivalTime;
        return this;
    }

    public FlightScheduledDomainEventBuilder SetRoute(string route)
    {
        _route = route;
        return this;
    }

    public FlightScheduledDomainEventBuilder SetDistance(int distance)
    {
        _distance = distance;
        return this;
    }

    public FlightScheduledDomainEventBuilder SetAircraftId(AircraftId aircraftId)
    {
        _aircraftId = aircraftId;
        return this;
    }

    public FlightScheduledDomainEventBuilder SetAvailableSeats(int availableSeats)
    {
        _availableSeats = availableSeats;
        return this;
    }

    public FlightScheduledDomainEventBuilder SetBookedSeats(int bookedSeats)
    {
        _bookedSeats = bookedSeats;
        return this;
    }

    public FlightScheduledDomainEventBuilder SetMaximumLuggageWeight(double maximumLuggageWeight)
    {
        _maximumLuggageWeight = maximumLuggageWeight;
        return this;
    }
}

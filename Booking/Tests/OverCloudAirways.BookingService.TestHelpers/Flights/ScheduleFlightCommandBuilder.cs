using OverCloudAirways.BookingService.Application.Flights.Commands.Schedule;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class ScheduleFlightCommandBuilder
{
    private FlightId _flightId = FlightId.New();
    private string _number = "AA123";
    private AirportId _departureAirportId = AirportId.New();
    private AirportId _destinationAirportId = AirportId.New();
    private DateTimeOffset _departureTime = Clock.Now.AddDays(7);
    private DateTimeOffset _arrivalTime = Clock.Now.AddDays(8);
    private string _route = "New York - Los Angeles";
    private int _distance = 7_000;
    private AircraftId _aircraftId = AircraftId.New();
    private int _availableSeats = 300;
    private double _maximumLuggageWeight = 35.0;

    public ScheduleFlightCommand Build()
    {
        return new ScheduleFlightCommand(
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

    public ScheduleFlightCommandBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public ScheduleFlightCommandBuilder SetNumber(string number)
    {
        _number = number;
        return this;
    }

    public ScheduleFlightCommandBuilder SetDepartureAirportId(AirportId departureAirportId)
    {
        _departureAirportId = departureAirportId;
        return this;
    }

    public ScheduleFlightCommandBuilder SetDestinationAirportId(AirportId destinationAirportId)
    {
        _destinationAirportId = destinationAirportId;
        return this;
    }

    public ScheduleFlightCommandBuilder SetDepartureTime(DateTimeOffset departureTime)
    {
        _departureTime = departureTime;
        return this;
    }

    public ScheduleFlightCommandBuilder SetArrivalTime(DateTimeOffset arrivalTime)
    {
        _arrivalTime = arrivalTime;
        return this;
    }

    public ScheduleFlightCommandBuilder SetRoute(string route)
    {
        _route = route;
        return this;
    }

    public ScheduleFlightCommandBuilder SetDistance(int distance)
    {
        _distance = distance;
        return this;
    }

    public ScheduleFlightCommandBuilder SetAircraftId(AircraftId aircraftId)
    {
        _aircraftId = aircraftId;
        return this;
    }

    public ScheduleFlightCommandBuilder SetAvailableSeats(int availableSeats)
    {
        _availableSeats = availableSeats;
        return this;
    }

    public ScheduleFlightCommandBuilder SetMaximumLuggageWeight(double maximumLuggageWeight)
    {
        _maximumLuggageWeight = maximumLuggageWeight;
        return this;
    }
}

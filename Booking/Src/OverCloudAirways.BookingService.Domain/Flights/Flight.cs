using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BookingService.Domain.Flights.Rules;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.Flights;

public class Flight : AggregateRoot<FlightId>
{
    public string Number { get; private set; }
    public AirportId DepartureAirportId { get; private set; }
    public AirportId DestinationAirportId { get; private set; }
    public DateTimeOffset DepartureTime { get; private set; }
    public DateTimeOffset ArrivalTime { get; private set; }
    public FlightStatus Status { get; private set; }
    public string Route { get; private set; }
    public int Distance { get; private set; }
    public AircraftId AircraftId { get; private set; }
    public int AvailableSeats { get; private set; }
    public int BookedSeats { get; private set; }
    public int ReservedSeats { get; private set; }
    public double MaximumLuggageWeight { get; private set; }
    public decimal? EconomyPrice { get; private set; }
    public decimal? FirstClassPrice { get; private set; }

    private Flight()
    {
    }

    public static async Task<Flight> ScheduleAsync(
        IAggregateRepository aggregateRepository,
        FlightId id,
        string number,
        AirportId departureAirportId,
        AirportId destinationAirportId,
        DateTimeOffset departureTime,
        DateTimeOffset arrivalTime,
        string route,
        int distance,
        AircraftId aircraftId,
        int availableSeats,
        double maximumLuggageWeight)
    {
        await CheckRuleAsync(new FlightCanOnlyBeScheduledInTheFutureRule(departureTime));
        await CheckRuleAsync(new TheAircraftsRangeMustBeGreaterThanTheFlightDistanceRule(distance, aircraftId, aggregateRepository));
        await CheckRuleAsync(new FlightMustBeScheduledBetweenTwoExistingAirportsInTheSystemRule(aggregateRepository, departureAirportId, destinationAirportId));

        var @event = new FlightScheduledDomainEvent(
            id, 
            number, 
            departureAirportId,
            destinationAirportId,
            departureTime, 
            arrivalTime, 
            route, 
            distance, 
            aircraftId, 
            availableSeats,
            maximumLuggageWeight);

        var flight = new Flight();
        flight.Apply(@event);

        return flight;
    }

    public async Task ChargePriceAsync(IFlightPriceCalculatorService priceCalculatorService)
    {
        await CheckRuleAsync(new OnlyScheduledFlightCanBeModifiedRule(this));
        var (economyPrice, firstClassPrice) = await priceCalculatorService.CalculateAsync(this);

        var @event = new FlightPriceChargedDomainEvent(Id, economyPrice, firstClassPrice);

        Apply(@event);
    }

    public async Task ReserveSeatsAsync(int seatsCount)
    {
        await CheckRuleAsync(new OnlyScheduledFlightCanBeModifiedRule(this));
        await CheckRuleAsync(new FlightMustHaveEnoughAvailableSeatsToReserveRule(this, seatsCount));

        var @event = new FlightSeatsReservedDomainEvent(Id, seatsCount);

        Apply(@event);
    }

    public async Task CancelAsync()
    {
        await CheckRuleAsync(new OnlyScheduledFlightCanBeModifiedRule(this));

        var @event = new FlightCanceledDomainEvent(Id);

        Apply(@event);
    }

    public async Task ChangeCapacityAsync(int capacity)
    {
        await CheckRuleAsync(new FlightCapacityMustBeGreaterEqualThanTotalBookedReservedSeatsRule(capacity, BookedSeats, ReservedSeats));

        var @event = new FlightCapacityChangedDomainEvent(Id, capacity);

        Apply(@event);
    }

    protected void When(FlightScheduledDomainEvent @event)
    {
        Id = @event.FlightId;
        Number = @event.Number;
        DepartureAirportId = @event.DepartureAirportId;
        DestinationAirportId = @event.DestinationAirportId;
        DepartureTime = @event.DepartureTime;
        ArrivalTime = @event.ArrivalTime;
        Status = FlightStatus.Scheduled;
        Route = @event.Route;
        Distance = @event.Distance;
        AircraftId = @event.AircraftId;
        AvailableSeats = @event.AvailableSeats;
        BookedSeats = 0;
        ReservedSeats = 0;
        MaximumLuggageWeight = @event.MaximumLuggageWeight;
    }

    protected void When(FlightPriceChargedDomainEvent @event)
    {
        EconomyPrice = @event.EconomyPrice;
        FirstClassPrice = @event.FirstClassPrice;
    }

    protected void When(FlightSeatsReservedDomainEvent @event)
    {
        AvailableSeats -= @event.SeatsCount;
        ReservedSeats += @event.SeatsCount;
    }

    protected void When(FlightCanceledDomainEvent _)
    {
        Status = FlightStatus.Cancelled;
    }

    protected void When(FlightCapacityChangedDomainEvent @event)
    {
        AvailableSeats = @event.Capacity - BookedSeats;
    }
}

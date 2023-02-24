using OverCloudAirways.BookingService.Domain._Shared;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.FlightBookings.Events;
using OverCloudAirways.BookingService.Domain.FlightBookings.Rules;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.FlightBookings;

public class FlightBooking : AggregateRoot<FlightBookingId>
{
    public CustomerId CustomerId { get; private set; }
    public FlightId FlightId { get; private set; }
    private List<Passenger> _passengers;
    public IReadOnlyCollection<Passenger> Passengers => _passengers.AsReadOnly();
    public FlightBookingStatus Status { get; private set; }

    private FlightBooking()
    {
        _passengers = new List<Passenger>();
    }

    public static async Task<FlightBooking> ReserveAsync(
        FlightBookingId flightBookingId,
        CustomerId customerId,
        Flight flight,
        IReadOnlyList<Passenger> passengers)
    {
        await CheckRuleAsync(new FlightBookingCanOnlyBeReservedForFlightsHasNotYetDepartedRule(flight));

        var @event = new FlightBookingReservedDomainEvent(flightBookingId, customerId, flight.Id, passengers);

        var flightBooking = new FlightBooking();
        flightBooking.Apply(@event);

        return flightBooking;
    }

    protected void When(FlightBookingReservedDomainEvent @event)
    {
        Id = @event.FlightBookingId;
        CustomerId = @event.CustomerId;
        FlightId = @event.FlightId;
        _passengers = new List<Passenger>(@event.Passengers);
        Status = FlightBookingStatus.Reserved;
    }
}

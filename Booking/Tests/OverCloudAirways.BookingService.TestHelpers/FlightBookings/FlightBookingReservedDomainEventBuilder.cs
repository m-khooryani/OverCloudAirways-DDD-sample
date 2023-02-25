using OverCloudAirways.BookingService.Domain._Shared;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.FlightBookings.Events;
using OverCloudAirways.BookingService.Domain.FlightBookings;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.TestHelpers._Shared;

namespace OverCloudAirways.BookingService.TestHelpers.FlightBookings;

public class FlightBookingReservedDomainEventBuilder
{
    private FlightBookingId _flightBookingId = FlightBookingId.New();
    private CustomerId _customerId = CustomerId.New();
    private FlightId _flightId = FlightId.New();
    private List<Passenger> _passengers = new List<Passenger>()
    {
        new PassengerBuilder().Build()
    };

    public FlightBookingReservedDomainEvent Build()
    {
        return new FlightBookingReservedDomainEvent(_flightBookingId, _customerId, _flightId, _passengers);
    }

    public FlightBookingReservedDomainEventBuilder SetFlightBookingId(FlightBookingId flightBookingId)
    {
        _flightBookingId = flightBookingId;
        return this;
    }

    public FlightBookingReservedDomainEventBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public FlightBookingReservedDomainEventBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public FlightBookingReservedDomainEventBuilder AddPassenger(Passenger passenger)
    {
        _passengers.Add(passenger);
        return this;
    }

    public FlightBookingReservedDomainEventBuilder SetPassengers(IEnumerable<Passenger> passengers)
    {
        _passengers.AddRange(passengers);
        return this;
    }
}

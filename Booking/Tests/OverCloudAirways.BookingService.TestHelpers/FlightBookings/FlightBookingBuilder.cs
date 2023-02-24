using OverCloudAirways.BookingService.Domain._Shared;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.FlightBookings;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.TestHelpers._Shared;

namespace OverCloudAirways.BookingService.TestHelpers.FlightBookings;

public class FlightBookingBuilder
{
    private FlightBookingId _flightBookingId = FlightBookingId.New();
    private CustomerId _customerId = CustomerId.New();
    private Flight _flight = null;
    private List<Passenger> _passengers = new List<Passenger>()
    {
        new PassengerBuilder().Build()
    };

    public async Task<FlightBooking> BuildAsync()
    {
        return await FlightBooking.ReserveAsync(_flightBookingId, _customerId, _flight, _passengers);
    }

    public FlightBookingBuilder SetFlightBookingId(FlightBookingId flightBookingId)
    {
        _flightBookingId = flightBookingId;
        return this;
    }

    public FlightBookingBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public FlightBookingBuilder SetFlight(Flight flight)
    {
        _flight = flight;
        return this;
    }

    public FlightBookingBuilder SetPassenger(Passenger passenger)
    {
        _passengers.Add(passenger);
        return this;
    }

    public FlightBookingBuilder SetPassengers(IEnumerable<Passenger> passengers)
    {
        _passengers.AddRange(passengers);
        return this;
    }
}

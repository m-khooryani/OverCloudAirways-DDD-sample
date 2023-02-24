using OverCloudAirways.BookingService.Domain._Shared;

namespace OverCloudAirways.BookingService.TestHelpers._Shared;

public class PassengerBuilder
{
    private string _firstName = "John";
    private string _lastName = "Doe";
    private string? _email = null;
    private string? _seatNumber = null;

    public Passenger Build()
    {
        return Passenger.Of(_firstName, _lastName, _email, _seatNumber);
    }

    public PassengerBuilder SetFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public PassengerBuilder SetLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public PassengerBuilder SetEmail(string? email)
    {
        _email = email;
        return this;
    }

    public PassengerBuilder SetSeatNumber(string? seatNumber)
    {
        _seatNumber = seatNumber;
        return this;
    }
}

using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain._Shared;

public class Passenger : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }
    private string? Email { get; }
    public string? SeatNumber { get; }

    private Passenger() 
    {
    }

    [JsonConstructor]
    private Passenger(string firstName, string lastName, string? email, string? seatNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        SeatNumber = seatNumber;
    }

    public static Passenger Of(string firstName, string lastName, string? email, string? seatNumber)
    {
        return new Passenger(firstName, lastName, email, seatNumber);
    }
}

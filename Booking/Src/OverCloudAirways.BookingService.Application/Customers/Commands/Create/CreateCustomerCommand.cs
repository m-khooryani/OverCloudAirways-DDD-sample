using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Customers.Commands.Create;

public record CreateCustomerCommand(
    CustomerId CustomerId,
    string FirstName,
    string LastName,
    string Email,
    DateOnly DateOfBirth,
    string PhoneNumber,
    string Address) : Command;

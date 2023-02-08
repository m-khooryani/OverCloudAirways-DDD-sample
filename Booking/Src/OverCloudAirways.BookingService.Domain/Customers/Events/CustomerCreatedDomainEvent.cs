using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.Customers.Events;

public record CustomerCreatedDomainEvent(
    CustomerId CustomerId,
    string FirstName,
    string LastName,
    string Email,
    DateOnly DateOfBirth,
    string PhoneNumber,
    string Address) : DomainEvent(CustomerId);

namespace OverCloudAirways.CrmService.Application.Customers.Queries.GetInfo;

public record CustomerDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateOnly DateOfBirth,
    string PhoneNumber,
    string Address,
    decimal LoyaltyPoints);

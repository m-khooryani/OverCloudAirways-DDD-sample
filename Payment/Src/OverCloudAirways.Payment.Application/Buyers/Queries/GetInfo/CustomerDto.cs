namespace OverCloudAirways.PaymentService.Application.Buyers.Queries.GetInfo;

public record BuyerDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.PaymentService.Domain.Buyers.Events;

public record BuyerRegisteredDomainEvent(
    BuyerId BuyerId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber) : DomainEvent(BuyerId);

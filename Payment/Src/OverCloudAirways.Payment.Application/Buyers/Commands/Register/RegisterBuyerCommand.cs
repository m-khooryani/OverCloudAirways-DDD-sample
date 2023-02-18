using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.Application.Buyers.Commands.Register;

public record RegisterBuyerCommand(
    BuyerId BuyerId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber) : Command;

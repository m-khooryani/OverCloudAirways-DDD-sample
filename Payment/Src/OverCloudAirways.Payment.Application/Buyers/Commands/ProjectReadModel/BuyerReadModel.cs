using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.PaymentService.Application.Buyers.Commands.ProjectReadModel;

internal record BuyerReadModel(
    Guid BuyerId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber) : ReadModel(BuyerId.ToString(), BuyerId.ToString());
using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.PaymentService.Application.Buyers.Commands.ProjectReadModel;

internal record BuyerReadModel(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber) : ReadModel(Id.ToString(), Id.ToString());
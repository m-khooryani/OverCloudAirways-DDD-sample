using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.CrmService.Application.Customers.Commands.ProjectReadModel;

internal record CustomerReadModel(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateOnly DateOfBirth,
    string PhoneNumber,
    string Address,
    decimal LoyaltyPoints) : ReadModel(Id.ToString(), Id.ToString());
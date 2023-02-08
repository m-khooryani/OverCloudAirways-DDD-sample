using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.BookingService.Application.Customers.Commands.ProjectReadModel;

internal record CustomerReadModel(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateOnly DateOfBirth,
    string PhoneNumber,
    string Address) : ReadModel(Id.ToString(), Id.ToString());
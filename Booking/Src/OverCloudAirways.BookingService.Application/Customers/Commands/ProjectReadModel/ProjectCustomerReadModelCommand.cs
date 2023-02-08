using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Customers.Commands.ProjectReadModel;

public record ProjectCustomerReadModelCommand(CustomerId CustomerId) : Command;

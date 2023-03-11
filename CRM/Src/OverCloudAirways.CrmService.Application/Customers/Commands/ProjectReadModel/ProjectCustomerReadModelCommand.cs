using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.CrmService.Domain.Customers;

namespace OverCloudAirways.CrmService.Application.Customers.Commands.ProjectReadModel;

public record ProjectCustomerReadModelCommand(CustomerId CustomerId) : Command;

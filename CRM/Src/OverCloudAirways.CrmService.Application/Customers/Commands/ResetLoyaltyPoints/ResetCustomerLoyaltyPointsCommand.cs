using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.CrmService.Domain.Customers;

namespace OverCloudAirways.CrmService.Application.Customers.Commands.ResetLoyaltyPoints;

public record ResetCustomerLoyaltyPointsCommand(CustomerId CustomerId) : Command;

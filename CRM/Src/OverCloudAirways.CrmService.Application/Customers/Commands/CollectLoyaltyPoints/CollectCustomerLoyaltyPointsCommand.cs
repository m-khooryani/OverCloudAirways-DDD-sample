using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.CrmService.Domain.Customers;

namespace OverCloudAirways.CrmService.Application.Customers.Commands.CollectLoyaltyPoints;

public record CollectCustomerLoyaltyPointsCommand(
    CustomerId CustomerId,
    decimal LoyaltyPoints) : Command;

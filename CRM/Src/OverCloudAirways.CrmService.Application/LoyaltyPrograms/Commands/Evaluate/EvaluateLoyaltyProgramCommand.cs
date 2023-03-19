using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.Evaluate;

public record EvaluateLoyaltyProgramCommand(
    LoyaltyProgramId LoyaltyProgramId,
    CustomerId CustomerId) : Command;

using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.Reactivate;

public record ReactivateLoyaltyProgramCommand(LoyaltyProgramId LoyaltyProgramId) : Command;

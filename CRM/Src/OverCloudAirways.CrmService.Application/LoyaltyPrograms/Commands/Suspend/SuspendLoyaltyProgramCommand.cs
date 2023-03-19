using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.Suspend;

public record SuspendLoyaltyProgramCommand(LoyaltyProgramId LoyaltyProgramId) : Command;

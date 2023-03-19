using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.ProjectReadModel;

public record ProjectLoyaltyProgramReadModelCommand(LoyaltyProgramId LoyaltyProgramId) : Command;

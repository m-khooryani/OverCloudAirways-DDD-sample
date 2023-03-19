using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Queries.GetInfo;

public record GetLoyaltyProgramInfoQuery(Guid LoyaltyProgramId) : Query<LoyaltyProgramDto>;

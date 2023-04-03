using System.Collections.ObjectModel;

namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

public interface IActiveLoyaltyPrograms
{
    Task<ReadOnlyCollection<LoyaltyProgramId>> GetLoyaltyProgramIds();
}
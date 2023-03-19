namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

public interface ILoyaltyProgramNameUniqueChecker
{
    Task<bool> IsUniqueAsync(string name);
}
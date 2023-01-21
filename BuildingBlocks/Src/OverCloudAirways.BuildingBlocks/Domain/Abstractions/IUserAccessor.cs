namespace OverCloudAirways.BuildingBlocks.Domain.Abstractions;

public interface IUserAccessor
{
    Guid UserId { get; }
    string FullName { get; }
}

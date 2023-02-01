namespace OverCloudAirways.BuildingBlocks.Domain.Abstractions;

public interface IUserAccessor
{
    Guid UserId { get; }
    string? TcpConnectionId { get; }
    string FullName { get; }
}

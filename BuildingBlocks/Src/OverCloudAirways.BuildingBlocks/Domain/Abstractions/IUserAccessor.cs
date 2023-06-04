namespace OverCloudAirways.BuildingBlocks.Domain.Abstractions;

public interface IUserAccessor
{
    Guid UserId { get; set; }
    string? TcpConnectionId { get; set; }
    string FullName { get; set; }
}

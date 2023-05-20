using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.IdentityService.API.GraphIntegration;

class B2CUserAccessor : IUserAccessor
{
    private static readonly Guid ConstantUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly string ConstantFullName = "Azure AD B2C";

    public Guid UserId => ConstantUserId;

    public string FullName => ConstantFullName;

    public string TcpConnectionId => string.Empty;
}


namespace OverCloudAirways.IdentityService.Infrastructure.DomainServices.Users;

internal class GraphConfiguration
{
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }
    public string TenantId { get; init; }
    public string Issuer { get; init; }
    public string ExtensionAppClientId { get; init; }
}

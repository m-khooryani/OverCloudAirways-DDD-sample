using Autofac;
using OverCloudAirways.IdentityService.Domain.Users;
using OverCloudAirways.IdentityService.Infrastructure.DomainServices.Users;

namespace OverCloudAirways.IdentityService.Infrastructure.DomainServices;

public class DomainServiceModule : Module
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _tenantId;

    public DomainServiceModule(string clientId, string clientSecret, string tenantId)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
        _tenantId = tenantId;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<GraphAPIUserRegistrationService>()
            .As<IGraphAPIUserRegistrationService>()
            .SingleInstance();

        var configuration = new GraphConfiguration()
        {
            ClientId = _clientId,
            ClientSecret = _clientSecret,
            TenantId = _tenantId
        };
        builder
            .RegisterInstance(configuration)
            .AsSelf()
            .SingleInstance();
    }
}

using Autofac;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;
using OverCloudAirways.CrmService.Infrastructure.DomainServices.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Infrastructure.DomainServices;

public class DomainServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<LoyaltyProgramNameUniqueChecker>()
            .As<ILoyaltyProgramNameUniqueChecker>()
            .SingleInstance();
    }
}

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using OverCloudAirways.IdentityService.API.GraphIntegration;

[assembly: FunctionsStartup(typeof(Startup))]
namespace OverCloudAirways.IdentityService.API.GraphIntegration;

internal class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddHttpClient();
    }
}


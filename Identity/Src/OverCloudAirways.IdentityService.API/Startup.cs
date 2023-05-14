using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using OverCloudAirways.IdentityService.API;

[assembly: FunctionsStartup(typeof(Startup))]
namespace OverCloudAirways.IdentityService.API;

internal class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddHttpClient();
    }
}

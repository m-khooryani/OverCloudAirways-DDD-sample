using Autofac;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.Json;

public class NewtonsoftJsonModule : Module
{
    private readonly JsonSerializerSettings _settings;

    public NewtonsoftJsonModule(JsonSerializerSettings settings)
    {
        _settings = settings;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterInstance(_settings)
            .AsSelf()
            .SingleInstance();

        builder.RegisterType<NewtonsoftJsonSerializer>()
            .As<IJsonSerializer>()
            .SingleInstance();
    }
}

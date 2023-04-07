using Autofac;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

public class CosmosNewtonsoftIntegrationModule : Module
{
    private readonly string _accountEndPoint;
    private readonly string _primaryKey;
    private readonly string _databaseId;
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public CosmosNewtonsoftIntegrationModule(
        string accountEndpoint,
        string primaryKey,
        string databaseId,
        JsonSerializerSettings jsonSerializerSettings)
    {
        _accountEndPoint = accountEndpoint;
        _primaryKey = primaryKey;
        _databaseId = databaseId;
        _jsonSerializerSettings = jsonSerializerSettings;
    }

    protected override void Load(ContainerBuilder builder)
    {
        var client = new CosmosClient(_accountEndPoint, _primaryKey, new CosmosClientOptions
        {
            Serializer = new CosmosJsonDotNetSerializer(_jsonSerializerSettings)
        });

        var database = client.GetDatabase(_databaseId);
        builder.RegisterInstance(database)
            .As<Database>()
            .SingleInstance();

        builder.RegisterType<CosmosManager>()
            .As<ICosmosManager>()
            .SingleInstance();
    }
}

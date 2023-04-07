using Autofac;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

public class CosmosDBModule : Module
{
    private readonly string _accountEndPoint;
    private readonly string _primaryKey;
    private readonly string _databaseId;

    public CosmosDBModule(
        string accountEndpoint,
        string primaryKey,
        string databaseId)
    {
        _accountEndPoint = accountEndpoint;
        _primaryKey = primaryKey;
        _databaseId = databaseId;
    }

    protected override void Load(ContainerBuilder builder)
    {
        var client = new CosmosClient(_accountEndPoint, _primaryKey, new CosmosClientOptions
        {
            Serializer = new CosmosJsonDotNetSerializer(new JsonSerializerSettings
            {
                Converters = new JsonConverter[]
                {
                    new EnumerationJsonConverter()
                },
                ContractResolver = new ValueObjectsConstructorResolver()
            })
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

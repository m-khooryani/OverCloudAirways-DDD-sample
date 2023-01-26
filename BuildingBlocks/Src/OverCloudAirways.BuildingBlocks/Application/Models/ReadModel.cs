using Newtonsoft.Json;

namespace OverCloudAirways.BuildingBlocks.Application.Models;

public record ReadModel
{
    [JsonProperty("id")]
    public string ItemId { get; private set; }
    [JsonProperty("partitionKey")]
    public string PartitionKey { get; private set; }

    public ReadModel(string id, string partitionKey)
    {
        ItemId = id;
        PartitionKey = partitionKey;
    }
}

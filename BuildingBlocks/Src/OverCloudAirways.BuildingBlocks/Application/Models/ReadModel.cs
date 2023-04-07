namespace OverCloudAirways.BuildingBlocks.Application.Models;

public record ReadModel
{
    public string id { get; private set; }
    public string partitionKey { get; private set; }

    public ReadModel(string id, string partitionKey)
    {
        this.id = id;
        this.partitionKey = partitionKey;
    }
}

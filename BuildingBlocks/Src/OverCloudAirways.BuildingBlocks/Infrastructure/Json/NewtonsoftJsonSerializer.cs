using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.Json;

internal class NewtonsoftJsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public NewtonsoftJsonSerializer(JsonSerializerSettings jsonSerializerSettings)
    {
        _jsonSerializerSettings = jsonSerializerSettings;
    }

    public string SerializeIndented(object? value)
    {
        return JsonConvert.SerializeObject(value, Formatting.Indented, _jsonSerializerSettings);
    }

    public string Serialize(object? value)
    {
        return JsonConvert.SerializeObject(value, _jsonSerializerSettings);
    }

    public object? Deserialize(string data, Type type)
    {
        return JsonConvert.DeserializeObject(data, type, _jsonSerializerSettings);
    }
}

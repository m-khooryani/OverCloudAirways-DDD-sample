using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.Json;

public class TypedIdJsonConverter : JsonConverter<TypedId>
{
    public TypedIdJsonConverter()
    {
    }

    public override void WriteJson(JsonWriter writer, TypedId value, JsonSerializer serializer)
    {
        if (value == null)
        {
            throw new JsonSerializationException("Expected TypedId object.");
        }

        writer.WriteValue(value.ToString());
    }

    public override TypedId ReadJson(JsonReader reader, Type objectType, TypedId existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        if (reader.TokenType != JsonToken.String)
        {
            throw new JsonSerializationException($"Unexpected token type: {reader.TokenType}. Expected string.");
        }

        var value = reader.Value.ToString();
        return (TypedId)TypedIdCreator.Create(value, objectType);
    }
}

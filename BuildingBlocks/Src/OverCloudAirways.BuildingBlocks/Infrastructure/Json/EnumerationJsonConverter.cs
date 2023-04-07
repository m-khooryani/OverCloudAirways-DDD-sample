using System.Reflection;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.Json;

public class EnumerationJsonConverter : JsonConverter<Enumeration>
{
    public override void WriteJson(JsonWriter writer, Enumeration value, JsonSerializer serializer)
    {
        if (value is null)
        {
            writer.WriteNull();
        }
        else
        {
            writer.WriteValue(value.Name);
        }
    }

    public override Enumeration ReadJson(JsonReader reader,
        Type objectType,
        Enumeration existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        return reader.TokenType switch
        {
            JsonToken.Integer => GetEnumerationFromJson(reader.Value.ToString(), objectType),
            JsonToken.String => GetEnumerationFromJson(reader.Value.ToString(), objectType),
            JsonToken.Null => null,

            _ => throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing an enumeration")
        };
    }

    private static Enumeration GetEnumerationFromJson(string nameOrValue, Type objectType)
    {
        try
        {
            object result = default;
            var methodInfo = typeof(Enumeration).GetMethod(
                nameof(Enumeration.TryGetFromValueOrName)
                , BindingFlags.Static | BindingFlags.Public);

            if (methodInfo == null)
            {
                throw new JsonSerializationException("Serialization is not supported");
            }

            var genericMethod = methodInfo.MakeGenericMethod(objectType);

            var arguments = new[] { nameOrValue, result };

            genericMethod.Invoke(null, arguments);
            return arguments[1] as Enumeration;
        }
        catch (Exception ex)
        {
            throw new JsonSerializationException($"Error converting value '{nameOrValue}' to a enumeration.", ex);
        }
    }
}
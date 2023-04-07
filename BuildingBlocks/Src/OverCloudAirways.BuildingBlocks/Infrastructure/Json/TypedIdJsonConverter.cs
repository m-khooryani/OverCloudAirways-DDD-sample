using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.Json;

public class TypedIdJsonConverter : JsonConverter
{
    public TypedIdJsonConverter()
    {
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var t = value.GetType();
        var val = t.GetProperty("Value").GetValue(value, null);
        writer.WriteValue(val);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string s = reader?.Value?.ToString() ?? "";
        if (string.IsNullOrWhiteSpace(s))
        {
            return default;
        }

        return TypedIdCreator.Create(s, objectType);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType.IsSubclassOfRawGeneric(typeof(TypedId<>));
    }

    public override bool CanRead
    {
        get { return true; }
    }
}

public static class TypeExtensions
{
    public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
    {
        while (toCheck != null && toCheck != typeof(object))
        {
            var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
            if (generic == cur)
            {
                return true;
            }
            toCheck = toCheck.BaseType;
        }
        return false;
    }
}

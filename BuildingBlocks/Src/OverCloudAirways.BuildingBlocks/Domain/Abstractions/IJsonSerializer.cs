namespace OverCloudAirways.BuildingBlocks.Domain.Abstractions;

public interface IJsonSerializer
{
    string Serialize(object? value);
    string SerializeIndented(object? value);
    object? Deserialize(string data, Type type);
}

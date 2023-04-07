using System.Reflection;
using Newtonsoft.Json.Serialization;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.Json;

public class ValueObjectsConstructorResolver : DefaultContractResolver
{
    protected override JsonObjectContract CreateObjectContract(Type objectType)
    {
        var contract = base.CreateObjectContract(objectType);

        if (!typeof(ValueObject).IsAssignableFrom(objectType))
        {
            return contract;
        }

        var overrideConstructor = GetMostSpecificConstructor(objectType);

        if (overrideConstructor != null)
        {
            SetOverrideCreator(contract, overrideConstructor);
        }

        return contract;
    }

    private void SetOverrideCreator(JsonObjectContract contract, ConstructorInfo attributeConstructor)
    {
        contract.OverrideCreator = CreateParameterizedConstructor(attributeConstructor);
        contract.CreatorParameters.Clear();
        foreach (var constructorParameter in base.CreateConstructorParameters(attributeConstructor, contract.Properties))
        {
            contract.CreatorParameters.Add(constructorParameter);
        }
    }

    private ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method)
    {
        var c = method as ConstructorInfo;
        if (c != null)
            return a => c.Invoke(a);
        return a => method.Invoke(null, a);
    }

    protected ConstructorInfo GetMostSpecificConstructor(Type objectType)
    {
        var constructors = objectType
            .GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .OrderBy(e => e.GetParameters().Length);

        var mostSpecific = constructors.LastOrDefault();
        return mostSpecific;
    }
}
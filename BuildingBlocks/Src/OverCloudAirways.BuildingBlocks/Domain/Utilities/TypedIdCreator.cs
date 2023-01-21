using System.ComponentModel;
using System.Linq.Expressions;

namespace OverCloudAirways.BuildingBlocks.Domain.Utilities;

internal static class TypedIdCreator
{
    public static Func<string, Type, object?> Create => (str, type) =>
    {
        var genericType = type.BaseType.GenericTypeArguments[0];
        var typedIdValue = TypeDescriptor.GetConverter(genericType)!.ConvertFromString(str)!;
        var targetConstructor = type.GetConstructor(new[] { genericType });
        var target = Expression.New(targetConstructor, Expression.Constant(typedIdValue));
        var lambda = Expression.Lambda(target);
        return lambda.Compile().DynamicInvoke();
    };
}

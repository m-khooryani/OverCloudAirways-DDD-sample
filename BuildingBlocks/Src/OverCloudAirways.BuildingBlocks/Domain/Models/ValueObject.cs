using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Exceptions;

namespace OverCloudAirways.BuildingBlocks.Domain.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    private PropertyInfo[] _properties;
    private FieldInfo[] _fields;

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        if (Equals(left, null))
        {
            return Equals(right, null);
        }
        return left.Equals(right);
    }

    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !(left == right);
    }

    public bool Equals(ValueObject other)
    {
        return Equals(other as object);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return GetProperties().All(propertyInfo => PropertiesAreEqual(obj, propertyInfo)) &&
            GetFields().All(fieldInfo => FieldsAreEqual(obj, fieldInfo));
    }

    private bool PropertiesAreEqual(object obj, PropertyInfo propertyInfo)
    {
        var type = propertyInfo.PropertyType;
        if (type.IsArray)
        {
            var left = propertyInfo.GetValue(this, null) as Array;
            var right = propertyInfo.GetValue(obj, null) as Array;
            return AreArraysEqual(left, right);
        }
        if (type.IsGenericType && (typeof(ICollection).IsAssignableFrom(type.GetGenericTypeDefinition()) ||
                                   typeof(ICollection<>).IsAssignableFrom(type.GetGenericTypeDefinition())))
        {
            var left = propertyInfo.GetValue(this, null) as ICollection;
            var right = propertyInfo.GetValue(obj, null) as ICollection;
            return AreCollectionsEqual(left, right);
        }
        return Equals(propertyInfo.GetValue(this, null), propertyInfo.GetValue(obj, null));
    }

    private static bool AreCollectionsEqual(ICollection left, ICollection right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null ||
            right is null ||
            left.Count != right.Count)
        {
            return false;
        }

        var leftEnumerator = left.GetEnumerator();
        var rightEnumerator = right.GetEnumerator();
        while (leftEnumerator.MoveNext() && rightEnumerator.MoveNext())
        {
            if (!Equals(leftEnumerator.Current, rightEnumerator.Current))
            {
                return false;
            }
        }

        return true;
    }

    private static bool AreArraysEqual(Array left, Array right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null ||
            right is null ||
            left.Length != right.Length)
        {
            return false;
        }

        for (int i = 0; i < left.Length; i++)
        {
            if (!Equals(left.GetValue(i), right.GetValue(i)))
            {
                return false;
            }
        }

        return true;
    }

    private bool FieldsAreEqual(object obj, FieldInfo fieldInfo)
    {
        var type = fieldInfo.FieldType;
        if (type.IsArray)
        {
            var left = fieldInfo.GetValue(this) as Array;
            var right = fieldInfo.GetValue(obj) as Array;
            return AreArraysEqual(left, right);
        }
        if (type.IsGenericType && (typeof(ICollection).IsAssignableFrom(type.GetGenericTypeDefinition()) ||
                                   typeof(ICollection<>).IsAssignableFrom(type.GetGenericTypeDefinition())))
        {
            var left = fieldInfo.GetValue(this) as ICollection;
            var right = fieldInfo.GetValue(obj) as ICollection;
            return AreCollectionsEqual(left, right);
        }
        return Equals(fieldInfo.GetValue(this), fieldInfo.GetValue(obj));
    }

    private IEnumerable<PropertyInfo> GetProperties()
    {
        if (_properties is null)
        {
            _properties = GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToArray();
        }

        return _properties;
    }

    private IEnumerable<FieldInfo> GetFields()
    {
        if (_fields is null)
        {
            _fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => p.GetCustomAttribute<CompilerGeneratedAttribute>() == null)
                .ToArray();
        }

        return _fields;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            foreach (var prop in GetProperties())
            {
                var value = prop.GetValue(this, null);
                hash = HashValue(hash, value);
            }

            foreach (var field in GetFields())
            {
                var value = field.GetValue(this);
                hash = HashValue(hash, value);
            }

            return hash;
        }
    }

    private static int HashValue(int seed, object value)
    {
        var currentHash = value?.GetHashCode() ?? 0;

        return seed * 23 + currentHash;
    }

    protected static async Task CheckRuleAsync(IBusinessRule rule)
    {
        if (!await rule.IsFollowedAsync())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}

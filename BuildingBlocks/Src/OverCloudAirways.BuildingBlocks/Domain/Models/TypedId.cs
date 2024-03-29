﻿using OverCloudAirways.BuildingBlocks.Domain.Exceptions;

namespace OverCloudAirways.BuildingBlocks.Domain.Models;

public class TypedId<TKey> : TypedId, IEquatable<TypedId<TKey>>
{
    public TKey Value { get; }

    public TypedId(TKey value)
    {
        if (value.Equals(default(TKey)) && typeof(TKey) != typeof(bool))
        {
            throw new TypedIdInitializationException("Id value cannot be empty!");
        }
        Value = value;
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }
        return obj is TypedId<TKey> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public bool Equals(TypedId<TKey> other)
    {
        return Value.Equals(other.Value) && GetType() == other.GetType();
    }

    public static bool operator ==(TypedId<TKey> left, TypedId<TKey> right)
    {
        if (Equals(left, null))
        {
            return Equals(right, null);
        }
        return left.Equals(right);
    }

    public static bool operator !=(TypedId<TKey> left, TypedId<TKey> right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator TKey(TypedId<TKey> typedId) => typedId.Value;
}

public class TypedId
{
    internal TypedId()
    {

    }
}

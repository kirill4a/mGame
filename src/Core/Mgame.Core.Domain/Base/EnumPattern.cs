using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mgame.Core.Domain.Base;

public abstract class EnumPattern : IComparable
{
    protected EnumPattern(int value, string name) => (Value, Name) = (value, name);

    public int Value { get; }
    public string Name { get; }

    public static IEnumerable<T> GetAll<T>()
        where T : EnumPattern
        =>
        typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();
    public int CompareTo(object other) => Value.CompareTo(((EnumPattern)other).Value);

    public override bool Equals(object obj)
    {
        if (obj is not EnumPattern other)
            return false;

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Value.Equals(other.Value);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Name;
}

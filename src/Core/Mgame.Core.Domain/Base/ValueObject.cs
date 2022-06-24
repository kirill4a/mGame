using System.Collections.Generic;
using System.Linq;

namespace Mgame.Core.Domain.Base;

// Learn more: https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects

/// <summary>
/// Base value object. 
/// Value objects are compare by their content (two VO are equal if their contents are strictly equal)
/// </summary>
public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (left is null ^ right is null)
            return false;
        return left is null || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right) => !(EqualOperator(left, right));

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;
        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode() => GetEqualityComponents()
                                        .Select(c => c != null ? c.GetHashCode() : 0)
                                        .Aggregate((x, y) => x ^ y);

    public static bool operator ==(ValueObject first, ValueObject second) => EqualOperator(first, second);
    public static bool operator !=(ValueObject first, ValueObject second) => NotEqualOperator(first, second);
}

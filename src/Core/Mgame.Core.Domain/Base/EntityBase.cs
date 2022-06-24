using System;
using System.Collections.Generic;
using Mgame.Core.Domain.Base.Interfaces;

namespace Mgame.Core.Domain.Base;

/// <summary>
/// Base domain entity
/// </summary>
/// <typeparam name="TId">Type of the key (id)</typeparam>
public abstract class EntityBase<TId> : IEntity<TId>
{
    public TId Id { get; protected set; }

    public DateTimeOffset CreatedOn { get; protected set; }

    protected EntityBase(TId id, DateTimeOffset createdOn)
    {
        Id = id;
        CreatedOn = createdOn;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;
        var other = (EntityBase<TId>)obj;
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode() => 2108858624 + Id.GetHashCode();

    public static bool operator ==(EntityBase<TId> left, EntityBase<TId> right)
    {
        return EqualityComparer<EntityBase<TId>>.Default.Equals(left, right);
    }

    public static bool operator !=(EntityBase<TId> left, EntityBase<TId> right)
    {
        return !(left == right);
    }
}

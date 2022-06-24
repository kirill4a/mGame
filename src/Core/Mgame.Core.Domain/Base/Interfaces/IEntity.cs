using System;

namespace Mgame.Core.Domain.Base.Interfaces;

/// <summary>
/// Keyed domain entity interface. 
/// Entities are compare by their ids (two entities are equal if their ids are equal)
/// </summary>
/// <typeparam name="TId">Type of the key (id)</typeparam>
public interface IEntity<TId>
{
    TId Id { get; }

    DateTimeOffset CreatedOn { get; }
}

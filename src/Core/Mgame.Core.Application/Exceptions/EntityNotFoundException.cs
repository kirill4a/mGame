using System;
using Mgame.Core.Domain.Base.Interfaces;

namespace Mgame.Core.Application.Exceptions;

public class EntityNotFoundException<TEntity, TId> : Exception where TEntity: IEntity<TId>
{
    public EntityNotFoundException(TId entityId)
        : base($"Entity ({typeof(TEntity).Name}) with id '{entityId}' is not found in repository")
    {
    }
}

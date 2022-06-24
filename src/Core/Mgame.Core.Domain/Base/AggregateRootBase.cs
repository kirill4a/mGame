using System;
using Mgame.Core.Domain.Base.Interfaces;

namespace Mgame.Core.Domain.Base;

/// <summary>
/// Base domain aggregate
/// </summary>
/// <typeparam name="TId">Type of the key (id)</typeparam>
public class AggregateRootBase<TId> : EntityBase<TId>, IAggregateRoot<TId>
{
    protected AggregateRootBase(TId id, DateTimeOffset createdOn) : base(id, createdOn)
    {
    }

    public void RaiseEvent()
    {
        //TODO: implement domain events system
        throw new NotImplementedException();
    }
}

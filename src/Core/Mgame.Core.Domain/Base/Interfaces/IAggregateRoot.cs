namespace Mgame.Core.Domain.Base.Interfaces;

/// <summary>
/// Aggregate root interface
/// </summary>
/// <typeparam name="TId">Type of the key (id)</typeparam>
public interface IAggregateRoot<TId> : IEntity<TId>
{
    void RaiseEvent();
}

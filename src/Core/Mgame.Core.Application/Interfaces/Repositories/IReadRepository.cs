using System;
using System.Linq;
using System.Linq.Expressions;

namespace Mgame.Core.Application.Interfaces.Repositories;

/// <summary>
/// Generic repository interface for reading operations (useful in CQRS)
/// </summary>
public interface IReadRepository<T>
{
    /// <summary>
    /// Reads all data from the storage
    /// </summary>
    /// <returns>Result as <see cref="IQueryable{T}"/></returns>
    IQueryable<T> GetAll();

    /// <summary>
    /// Reads data that matches the given condition
    /// </summary>
    /// <param name="predicate">Condition</param>
    /// <returns>Result as <see cref="IQueryable{T}"/></returns>
    IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
}

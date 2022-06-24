using System.Threading.Tasks;
using Mgame.Core.Domain.Base.Interfaces;

namespace Mgame.Core.Application.Interfaces.Repositories;

/// <summary>
/// Generic entity repository for domain entities. See <see cref="IEntity{TId}">
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
/// <typeparam name="TId">Entity identifier type</typeparam>
public interface IEntityRepository<T, TId> : IReadRepository<T>, IWriteRepository<T> where T : IEntity<TId>
{
    /// <summary>
    /// Finds the entity by id in the storage
    /// </summary>
    /// <param name="id">Entity id</param>
    /// <returns>Entity</returns>
    T Find(TId id);

    /// <summary>
    /// Asynchronously finds the entity by id in the storage
    /// </summary>
    /// <param name="id">Entity id</param>
    /// <returns>Entity</returns>
    Task<T> FindAsync(TId id);
}

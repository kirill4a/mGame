using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mgame.Core.Application.Interfaces.Repositories;

/// <summary>
/// Generic repository interface for writing operations (useful in CQRS)
/// </summary>
public interface IWriteRepository<T>
{
    /// <summary>
    /// Adds the object to the storage
    /// </summary>
    /// <param name="value">Object to add</param>
    void Add(T value);

    /// <summary>
    /// Asynchronously adds the object to the storage
    /// </summary>
    /// <param name="value">Object to add</param>
    Task AddAsync(T value);

    /// <summary>
    /// Adds the collection of objects to the storage
    /// </summary>
    /// <param name="value">Collection of objects to add</param>
    void AddRange(IEnumerable<T> values);

    /// <summary>
    /// Asynchronously adds the collection of objects to the storage
    /// </summary>
    /// <param name="value">Collection of objects to add</param>
    Task AddRangeAsync(IEnumerable<T> values);

    /// <summary>
    /// Updates the object in the storage
    /// </summary>
    /// <param name="value">Object to update</param>
    void Update(T value);

    /// <summary>
    /// Asynchronously updates the object in the storage
    /// </summary>
    /// <param name="value">Object to update</param>
    Task UpdateAsync(T value);

    /// <summary>
    /// Deletes the object from the storage
    /// </summary>
    /// <param name="value">Object to delete</param>
    void Delete(T value);

    /// <summary>
    /// Asynchronously deletes the object from the storage
    /// </summary>
    /// <param name="value">Object to delete</param>
    Task DeleteAsync(T value);
}

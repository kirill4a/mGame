using System.Threading;
using System.Threading.Tasks;

namespace Mgame.Core.Application.Interfaces.Repositories;

/// <summary>
/// UnitOfWork pattern of repository for logical transactions
/// </summary>
public interface IUnitOfWork
{
    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

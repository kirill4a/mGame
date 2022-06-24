using System;
using Mgame.Core.Domain.Aggregates.Room;

namespace Mgame.Core.Application.Interfaces.Repositories.Domain;

public interface IRoomRepository : IEntityRepository<BaseRoom, Guid>
{
    bool AnyOpenedRoom(Guid playerId);
}

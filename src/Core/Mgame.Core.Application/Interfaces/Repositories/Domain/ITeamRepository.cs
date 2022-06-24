using System;
using Mgame.Core.Domain.Entities.Team;

namespace Mgame.Core.Application.Interfaces.Repositories.Domain;

public interface ITeamRepository : IEntityRepository<BaseTeam, Guid>
{
   
}

using System;
using Mgame.Core.Domain.Entities.Team;

namespace Mgame.Core.Domain.Exceptions;

public class TeamCapacityExceededException : Exception
{
    public TeamCapacityExceededException(BaseTeam team)
        : base($"Team {team.Id} has been reached maximum players capacity of {team.Capacity}")
    {
    }
}

using System;
using Mgame.Core.Domain.Entities;
using Mgame.Core.Domain.Entities.Team;

namespace Mgame.Core.Domain.Exceptions;

public class PlayerAlreadyInTeamException : Exception
{
    public PlayerAlreadyInTeamException(Player player, BaseTeam team)
        : base($"Team '{team.Id}' already contains player '{player.Id}'")
    {
    }
}

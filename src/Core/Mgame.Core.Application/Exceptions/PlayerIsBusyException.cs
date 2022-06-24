using System;
using Mgame.Core.Domain.Entities;

namespace Mgame.Core.Application.Exceptions;

public class PlayerIsBusyException : Exception
{
    public PlayerIsBusyException(Player player)
        : base($"Player '{player.Id}' is already in game (joined team, playing, etc)")
    {
    }
}

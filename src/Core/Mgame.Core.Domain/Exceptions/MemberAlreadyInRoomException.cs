using System;
using Mgame.Core.Domain.Aggregates.Room;
using Mgame.Core.Domain.Entities;

namespace Mgame.Core.Domain.Exceptions;

public class MemberAlreadyInRoomException : Exception
{
    public MemberAlreadyInRoomException(Member member, BaseRoom room)
        : base($"Room '{room.Id}' already contains member ({member.GetType().Name}) '{member.Id}'")
    {
    }
}

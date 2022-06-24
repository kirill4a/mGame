using System;
using Mgame.Core.Domain.Aggregates.Room;

namespace Mgame.Core.Domain.Exceptions;

public class RoomCapacityExceededException : Exception
{
    public RoomCapacityExceededException(BaseRoom room)
        : base($"Room {room.Id} has been reached maximum players capacity")
    {
    }
}
